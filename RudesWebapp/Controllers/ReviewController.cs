using System;
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
using RudesWebapp.Models;
namespace RudesWebapp.Controllers
{
    [Authorize(Roles = "Coach, Board, Admin")]
    public class ReviewController : Controller
    {
        private readonly RudesDatabaseContext _context;
        private readonly IMapper _mapper;

        public ReviewController(RudesDatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Review
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ReviewDTO>>(await _context.Review.ToListAsync()));
        }
        // GET: Review/Details/5
        public async Task<IActionResult> Details(int? id)
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

            return View(_mapper.Map<ReviewDTO>(review));
        }

        // GET: Review/Create
        public async Task<IActionResult> Create()
        {
            await PrepareDropDowns();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ArticleId,Rating,Comment,UserId,Blocked")]
            ReviewDTO reviewDTO)
        {
            if (ModelState.IsValid)
            {
                
                _context.Add(_mapper.Map<Review>(reviewDTO));
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            await PrepareDropDowns();
            return View(reviewDTO);
        }

        // GET: Player/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Review.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            await PrepareDropDowns();
            return View(_mapper.Map<ReviewDTO>(review));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ArticleId,Rating,Comment,UserId,Blocked")]
            ReviewDTO reviewDTO)
        {
            if (id != reviewDTO.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // NOTE: https://stackoverflow.com/questions/13314666/using-automapper-to-update-an-existing-entity-poco/25242322
                    var review = await _context.Review.FindAsync(id);
                    _mapper.Map(reviewDTO, review);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewExists(reviewDTO.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            await PrepareDropDowns();
            return View(reviewDTO);
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

            return View(_mapper.Map<ReviewDTO>(review));
        }
        // POST: Review/Delete/3
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var review = await _context.Review.FindAsync(id);
            _context.Review.Remove(review);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task PrepareDropDowns()
        {
            var images = await _context.Image.ToListAsync();
            ViewBag.Images = new SelectList(images, nameof(Image.Id), nameof(Image.Name));
        }
        private bool ReviewExists(int id)
        {
            return _context.Review.Any(e => e.Id == id);
        }
    }
}