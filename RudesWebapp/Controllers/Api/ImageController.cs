using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RudesWebapp.Data;
using RudesWebapp.Dtos;

namespace RudesWebapp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly RudesDatabaseContext _context;
        private readonly IMapper _mapper;

        public ImageController(RudesDatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Image/2
        [HttpGet("{id}")]
        public async Task<ActionResult<ImageDTO>> GetImage(int id)
        {
            var image = await _context.Image.FindAsync(id);
            if (image == null)
            {
                return NotFound();
            }

            return _mapper.Map<ImageDTO>(image);
        }
    }
}