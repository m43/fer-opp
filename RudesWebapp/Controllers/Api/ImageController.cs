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
    public class ImageController: ControllerBase
    {
        private readonly RudesDatabaseContext _context;
        private readonly IMapper _mapper;

        public ImageController(RudesDatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImageDTO>>> GetImages()
        {
            return _mapper.Map<List<ImageDTO>>(await _context.Image.ToListAsync());
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

        [HttpPost]
        [Authorize(Roles = "Admin, Board, Coach")]
        public async Task<IActionResult> SetImage(ImageDTO imageDto)
        {
            var image = _mapper.Map<Image>(imageDto);
            _context.Image.Add(image);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetImage", new { id = imageDto.Id }, imageDto);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Board, Coach")]
        public async Task<ActionResult<ImageDTO>> DeleteImage(int id)
        {
            var image = await _context.Image.FindAsync(id);
            if (image == null)
            {
                return NotFound();
            }

            _context.Image.Remove(image);
            await _context.SaveChangesAsync();

            return _mapper.Map<ImageDTO>(image);
        }
    }
}