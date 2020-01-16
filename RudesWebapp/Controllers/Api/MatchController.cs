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
    public class MatchController : ControllerBase
    {
        private readonly RudesDatabaseContext _context;
        private readonly IMapper _mapper;

        public MatchController(RudesDatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MatchDTO>>> GetMatches()
        {
            return _mapper.Map<List<MatchDTO>>(await _context.Match.ToListAsync());
        }

        // GET: api/Match/2
        [HttpGet("{id}")]
        public async Task<ActionResult<MatchDTO>> GetMatch(int id)
        {
            var match = await _context.Match.FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }

            return _mapper.Map<MatchDTO>(match);
        }
    }
}