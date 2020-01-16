using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RudesWebapp.Data;
using RudesWebapp.Dtos;

namespace RudesWebapp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly RudesDatabaseContext _context;
        private readonly IMapper _mapper;

        public DiscountController(RudesDatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiscountDTO>>> GetDiscounts()
        {
            return _mapper.Map<List<DiscountDTO>>(await _context.Discount.ToListAsync());
        }

        // GET: api/Discount/1
        [HttpGet("{id}")]
        public async Task<ActionResult<DiscountDTO>> GetDiscount(int id)
        {
            var discount = await _context.Discount.FindAsync(id);
            if (discount == null)
            {
                return NotFound();
            }

            return _mapper.Map<DiscountDTO>(discount);
        }
    }
}