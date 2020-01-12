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
        public async Task<IActionResult> PostOrder(EditOrderDTO editOrderDto)
        {
            var order = _mapper.Map<Order>(editOrderDto);
            _context.Order.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new {id = editOrderDto.Id}, editOrderDto);
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