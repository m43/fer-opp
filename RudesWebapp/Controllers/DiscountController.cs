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
    [Authorize(Roles = Roles.BoardOrAbove)]
    public class DiscountController : Controller
    {
        private readonly RudesDatabaseContext _context;
        private readonly IMapper _mapper;

        public DiscountController(RudesDatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Discount
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<DiscountDTO>>(await _context.Discount.ToListAsync()));
        }

        // GET: Discount/Details/5
        public async Task<IActionResult> Details(int? id, int articleId)
        {
            if (articleId == 0)
            {
                return NotFound();
            }

            if (id == null)
            {
                return NotFound();
            }

            var discount = await _context.Discount.FirstOrDefaultAsync(m => m.Id == id && m.ArticleId == articleId);
            if (discount == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<DiscountDTO>(discount));
        }

        public async Task<IActionResult> Create()
        {
            await PrepareDropDowns();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ArticleId,StartDate,EndDate,Percentage")]
            DiscountDTO discountDto)
        {
            if (ModelState.IsValid)
            {
                _context.Add((object) _mapper.Map<Discount>(discountDto));
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            await PrepareDropDowns();
            return View(discountDto);
        }

        // GET: Player/Edit/5
        public async Task<IActionResult> Edit(int? id, int articleId)
        {
            if (articleId == 0)
            {
                return BadRequest();
            }

            if (id == null)
            {
                return NotFound();
            }

            var discount = await _context.Discount.FindAsync(id, articleId);
            if (discount == null)
            {
                return NotFound();
            }

            await PrepareDropDowns();
            return View(_mapper.Map<DiscountDTO>(discount));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int articleId, [Bind("Id,ArticleId,StartDate,EndDate,Percentage")]
            DiscountDTO discountDto)
        {
            if (articleId == 0)
            {
                return NotFound();
            }

            if (id != discountDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // NOTE: https://stackoverflow.com/questions/13314666/using-automapper-to-update-an-existing-entity-poco/25242322
                    var discount = await _context.Discount.FindAsync(id, articleId);
                    _mapper.Map(discountDto, discount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiscountExists(discountDto.Id))
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
            return View(discountDto);
        }

        // GET: Review/Delete/4
        public async Task<IActionResult> Delete(int? id, int articleId)
        {
            if (articleId == 0)
            {
                return BadRequest();
            }

            if (id == null)
            {
                return NotFound();
            }

            var discount = await _context.Discount.FirstOrDefaultAsync(m => m.Id == id && m.ArticleId == articleId);
            if (discount == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<DiscountDTO>(discount));
        }

        // POST: Review/Delete/3
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int articleId)
        {
            var discount = await _context.Discount.FindAsync(id, articleId);
            _context.Discount.Remove(discount);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task PrepareDropDowns()
        {
            var articles = await _context.Article.ToListAsync();
            ViewBag.Articles = new SelectList(articles, nameof(Article.Id), nameof(Article.Name));
        }

        private bool DiscountExists(int id)
        {
            return _context.Discount.Any(e => e.Id == id);
        }
    }
}