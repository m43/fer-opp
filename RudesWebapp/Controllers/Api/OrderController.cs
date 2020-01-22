using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RudesWebapp.Data;
using RudesWebapp.Dtos;
using RudesWebapp.Helpers;
using RudesWebapp.Models;
using RudesWebapp.Services;

namespace RudesWebapp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly RudesDatabaseContext _context;
        private readonly IMapper _mapper;

        public OrderController(RudesDatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EditOrderDTO>>> GetOrders()
        {
            return _mapper.Map<List<EditOrderDTO>>(await _context.Order.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EditOrderDTO>> GetOrder(int id)
        {
            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            return _mapper.Map<EditOrderDTO>(order);
        }

        [HttpPost]
        [Authorize(Roles = Roles.UserOrAbove)]
        public async Task<ActionResult<Order>> PostOrder([FromBody] OrderDTO orderDTO)
        {
            if (orderDTO.Items.Count() == 0)
            {
                return null;
            }

            await Task.Delay(5000); // Payment simulation - just wait 5 seconds

            // Using a random number generator to decide whether or not the transaction will succeed.
            // Relax, it's just a simulation :)
            Random random = new Random();
            int randomNumber = random.Next();

            if (randomNumber % 4 == 0)
            {
                // TODO put something more appropriate
                return null; // returns null if the transaction failed
            }

            var orderArticles = new List<OrderArticle>();
            var items = orderDTO.Items;
            
            if (items.Count() == 0)
            {
                return null;
            }

            foreach (var item in items)
            {
                
                var availability = await _context.ArticleAvailability
                    .FindAsync(item.ArticleId, item.Size);
                if (availability != null)
                {
                    if (item.Quantity > availability.Quantity)
                    {
                        return NoContent(); // TODO change to something more appropriate
                    }
                }
                else
                {
                    return NoContent(); // TODO change to something more appropriate
                }
                availability.Quantity -= item.Quantity;
                await _context.SaveChangesAsync();
                
                orderArticles.Add(_mapper.Map<OrderArticle>(new OrderArticleDTO
                {
                    ArticleId = item.ArticleId,
                    Quantity = item.Quantity,
                    Size = item.Size,
                    PurchaseDiscount = item.Percentage,
                    PurchasePrice = item.Price
                }));
            }

            // Remove order articles from the shopping cart
            var shoppingCartId = items.ElementAt(0).ShoppingCartId;

            foreach (var orderArticle in orderArticles)
            {
                var shoppingCartArticle = await _context.ShoppingCartArticle
                    .Where(scArticle => 
                        scArticle.ShoppingCartId == shoppingCartId &&
                        scArticle.ArticleId == orderArticle.ArticleId &&
                        scArticle.Size == orderArticle.Size)
                    .FirstOrDefaultAsync();

                if (shoppingCartArticle.Quantity - (orderArticle.Quantity ?? 0) == 0)
                {
                    _context.ShoppingCartArticle.Remove(shoppingCartArticle);
                }
                else
                {
                    shoppingCartArticle.Quantity -= (orderArticle.Quantity ?? 0);
                }
            }
            await _context.SaveChangesAsync();

            var order = new Order
            {
                Fulfilled = false,
                Address = orderDTO.Address,
                City = orderDTO.City,
                PostalCode = orderDTO.PostalCode,
                OrderArticle = orderArticles,
                UserId = User.GetUserId()
            };
            _context.Order.Add(order);
            await _context.SaveChangesAsync();

            order.OrderArticle = null;
            return Ok(order);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.BoardOrAbove)]
        public async Task<ActionResult<EditOrderDTO>> DeleteOrder(int id)
        {
            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Order.Remove(order);
            await _context.SaveChangesAsync();

            return _mapper.Map<EditOrderDTO>(order);
        }
    }
}