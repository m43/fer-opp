using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RudesWebapp.Data;
using RudesWebapp.Dtos;
using RudesWebapp.Models;
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
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders()
        {
            return _mapper.Map<List<OrderDTO>>(await _context.Order.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> GetOrder(int id)
        {
            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            return _mapper.Map<OrderDTO>(order);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Board, Coach, User")]
        public async Task<IActionResult> PostOrder(OrderDTO orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
            _context.Order.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = orderDto.Id }, orderDto);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Board, Coach, User")]
        public async Task<ActionResult<OrderDTO>> DeleteOrder(int id)
        {
            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Order.Remove(order);
            await _context.SaveChangesAsync();

            return _mapper.Map<OrderDTO>(order);
        }
    }
}
