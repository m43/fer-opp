using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RudesWebapp.Data;
using RudesWebapp.Dtos;
using RudesWebapp.Helpers;
using RudesWebapp.Models;

namespace RudesWebapp.Controllers
{
    [Authorize(Roles = Roles.UserOrAbove)]
    public class AddReviewController : Controller
    {
        private readonly RudesDatabaseContext _context;
        private readonly IMapper _mapper;

        public AddReviewController(RudesDatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Review
        public async Task<IActionResult> Index()
        {
            var reviews = await _context.Review
                .Where(r => r.UserId == User.GetUserId() && !r.Blocked)
                .Include(u => u.Article)
                .ToListAsync();

            return View(reviews);
        }

        // GET: Review/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Review.Include(a => a.Article).FirstOrDefaultAsync(m => m.Id == id);
            if (review == null)
            {
                return NotFound();
            }

            if (review.UserId != User.GetUserId() || review.Blocked)
            {
                return NotFound();
            }

            return View(review);
        }

        // GET: Review/Create
        public async Task<IActionResult> Create()
        {
            await PrepareDropDowns();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ArticleId,Rating,Comment")] AddReviewDTO addReviewDto)
        {
            if (addReviewDto.Id != 0)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (!(await GetReviewableIds()).Contains(addReviewDto.ArticleId))
                {
                    return NotFound();
                }

                var review = _mapper.Map<Review>(addReviewDto);
                review.UserId = User.GetUserId();
                await _context.AddAsync(review);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            await PrepareDropDowns();
            return View(addReviewDto);
        }

        // GET: Review/Delete/4
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Review.FirstOrDefaultAsync(m => m.Id == id);
            if (review == null)
            {
                return NotFound();
            }

            if (review.UserId != User.GetUserId() || review.Blocked)
            {
                return NotFound();
            }

            return View(review);
        }

        // POST: Review/Delete/3
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var review = await _context.Review.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            if (review.UserId != User.GetUserId() || review.Blocked)
            {
                return NotFound();
            }

            _context.Review.Remove(review);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<IEnumerable<int>> GetReviewableIds()
        {
            var articlesOrdered = await _context.Order
                .Include(o => o.OrderArticle)
                .Where(o => o.Fulfilled && o.UserId == User.GetUserId())
                .Select(
                    o => o.OrderArticle
                        .Where(oa => oa.ArticleId != null)
                        .Select(oa => oa.ArticleId.Value)
                        .Distinct())
                .SelectMany(ids => ids)
                .GroupBy(ids => ids)
                .Select(ids => new {ids.Key, Count = ids.Count()})
                .ToDictionaryAsync(ids => ids.Key, ids => ids.Count);

            var articlesReviewed = await _context.Review
                .Where(r => r.UserId != null && r.UserId == User.GetUserId())
                .GroupBy(r => r.ArticleId)
                .Select(r => new {r.Key, Count = r.Count()})
                .ToDictionaryAsync(reviews => reviews.Key, reviews => reviews.Count);

            var reviewableIds = articlesOrdered
                .Where(a => a.Value > articlesReviewed.GetValueOrDefault(a.Key, 0))
                .Select(a => a.Key);

            return reviewableIds;
        }

        private async Task PrepareDropDowns()
        {
            var reviewableIds = await GetReviewableIds();
            var articles = await _context.Article.Where(a => reviewableIds.Contains(a.Id)).ToListAsync();
            
            ViewBag.Articles = new SelectList(articles, nameof(Article.Id), nameof(Article.Name));
        }
    }
}