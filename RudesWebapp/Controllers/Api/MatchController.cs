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

        [HttpPost]
        [Authorize(Roles = "Admin, Board, Coach")]
        public async Task<IActionResult> SetMatch(MatchDTO matchDto)
        {
            var match = _mapper.Map<Match>(matchDto);
            _context.Match.Add(match);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMatch", new {id = matchDto.Id}, matchDto);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Board, Coach")]
        public async Task<ActionResult<MatchDTO>> DeleteMatch(int id)
        {
            var match = await _context.Match.FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }

            _context.Match.Remove(match);
            await _context.SaveChangesAsync();

            return _mapper.Map<MatchDTO>(match);
        }
    }
}