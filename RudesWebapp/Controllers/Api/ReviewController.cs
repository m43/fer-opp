using System;
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
        public async Task<ActionResult<IEnumerable<ReviewDTO>>> GetReviews()
        {
            return _mapper.Map<List<ReviewDTO>>(await _context.Review.ToListAsync());
        }

        // GET: api/Review/1
        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewDTO>> GetReviewDTO(int id)
        {
            var review = await _context.Review.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            return _mapper.Map<ReviewDTO>(review);
        }
        [HttpPost]
        [Authorize(Roles = "User")] //provjeriti
        public async Task<IActionResult> PostReview(ReviewDTO reviewDTO)
        {
            var review = _mapper.Map<Review>(reviewDTO);
            _context.Review.Add(review);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReview", new { id = reviewDTO.Id }, reviewDTO);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ReviewDTO>> DeleteReview(int id)
        {
            var review = await _context.Review.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            _context.Review.Remove(review);
            await _context.SaveChangesAsync();

            return _mapper.Map<ReviewDTO>(review);
        }

        

    }
}
