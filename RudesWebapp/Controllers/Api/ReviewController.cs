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
        public async Task<ActionResult<IEnumerable<AddReviewDTO>>> GetReviews()
        {
            return _mapper.Map<List<AddReviewDTO>>(await _context.Review.Where(r => !r.Blocked).ToListAsync());
        }

        // GET: api/Review/1
        [HttpGet("{id}")]
        public async Task<ActionResult<AddReviewDTO>> GetReview(int id)
        {
            var review = await _context.Review.FindAsync(id);
            if (review == null || review.Blocked)
            {
                return NotFound();
            }

            return _mapper.Map<AddReviewDTO>(review);
        }

        [HttpPost]
        [Authorize(Roles = Roles.UserOrAbove)]
        public async Task<IActionResult> PostReview(AddReviewDTO addReviewDto)
        {
            // TODO validation
            // TODO additional checks - can the user add a review at all?

            var review = _mapper.Map<Review>(addReviewDto);
            _context.Review.Add(review);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReview", new {id = addReviewDto.Id}, addReviewDto);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.UserOrAbove)]
        public async Task<ActionResult<AddReviewDTO>> DeleteReview(int id)
        {
            // TODO validation
            // TODO additional checks - is this the user that wrote the review?
            // TODO additional checks - can the user delete this review at all?

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