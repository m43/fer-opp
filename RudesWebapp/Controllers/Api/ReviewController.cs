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
    public class ReviewController : ControllerBase
    {
        private readonly RudesDatabaseContext _context;
        private readonly IMapper _mapper;

        public ReviewController(RudesDatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Review
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddReviewDTO>>> GetReviews()
        {
            return _mapper.Map<List<AddReviewDTO>>(await _context.Review.ToListAsync());
        }

        // GET: api/Review/1
        [HttpGet("{id}")]
        public async Task<ActionResult<AddReviewDTO>> GetReview(int id)
        {
            var review = await _context.Review.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            return _mapper.Map<AddReviewDTO>(review);
        }

        [HttpPost]
        [Authorize(Roles = "User")] //provjeriti
        public async Task<IActionResult> PostReview(AddReviewDTO addReviewDto)
        {
            var review = _mapper.Map<Review>(addReviewDto);
            _context.Review.Add(review);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReview", new {id = addReviewDto.Id}, addReviewDto);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<AddReviewDTO>> DeleteReview(int id)
        {
            var review = await _context.Review.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            _context.Review.Remove(review);
            await _context.SaveChangesAsync();

            return _mapper.Map<AddReviewDTO>(review);
        }
    }
}