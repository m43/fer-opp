using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RudesWebapp.Data;
using RudesWebapp.Dtos;
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
        public async Task<IActionResult> PostOrder([FromBody] OrderDTO orderDTO)
        {
            var orderArticles = new List<OrderArticle>();
            var items = new List<ItemDTO>();
            
            foreach (var article in items)
            {
                
                var availability = await _context.ArticleAvailability.FindAsync(article.ArticleId, article.Size);
                if (availability != null)
                {
                    if (article.Quantity > availability.Quantity)
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return NotFound();
                }
                availability.Quantity -= article.Quantity;
                await _context.SaveChangesAsync();
                
                orderArticles.Add(_mapper.Map<OrderArticle>(new OrderArticleDTO
                {
                    ArticleId = article.ArticleId,
                    Quantity = article.Quantity,
                    Size = article.Size,
                    PurchaseDiscount = article.Percentage,
                    PurchasePrice = article.Price
                }));
            }
            var order = new Order
            {
                Fulfilled = false,
                Address = orderDTO.Address,
                City = orderDTO.City,
                PostalCode = orderDTO.PostalCode,
                OrderArticle = orderArticles
            };
            _context.Order.Add(order);
            await _context.SaveChangesAsync();
        
            return Ok(order);
            
            // return CreatedAtAction("GetOrder", new {id = editOrderDto.Id}, editOrderDto);
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