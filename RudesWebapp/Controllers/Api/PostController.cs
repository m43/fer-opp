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
    public class PostController : ControllerBase
    {
        private readonly RudesDatabaseContext _context;
        private readonly IMapper _mapper;

        public PostController(RudesDatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Post
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GetPost()
        {
            return _mapper.Map<List<PostDTO>>(await _context.Post.Include(p => p.Image).ToListAsync());
        }

        // GET: api/Post/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PostDTO>> GetPostDto(int id)
        {
            var post = await _context.Post.Include(p => p.Image).FirstAsync(p => p.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return _mapper.Map<PostDTO>(post);
        }
    }
}