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

namespace RudesWebapp.Controllers
{
    [Authorize(Roles = Roles.CoachOrAbove)]
    public class ImageController : Controller
    {
        private readonly RudesDatabaseContext _context;
        private readonly IMapper _mapper;

        public ImageController(RudesDatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ImageDTO>>(await _context.Image.ToListAsync()));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var image = await _context.Image.FirstOrDefaultAsync(m => m.Id == id);
            if (image == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<ImageDTO>(image));
        }

        // GET: Image/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,OriginalName,Caption,AltText")]
            ImageDTO imageDto)
        {
            if (ModelState.IsValid)
            {
                _context.Add((object) _mapper.Map<Image>(imageDto));
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(imageDto);
        }

        // GET: Image/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var image = await _context.Image.FindAsync(id);
            if (image == null)
            {
                return NotFound();
            }


            return View(_mapper.Map<ImageDTO>(image));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,OriginalName,Caption,AltText")]
            ImageDTO imageDto)
        {
            if (id != imageDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // NOTE: https://stackoverflow.com/questions/13314666/using-automapper-to-update-an-existing-entity-poco/25242322
                    var image = await _context.Image.FindAsync(id);
                    _mapper.Map(imageDto, image);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImageExists(imageDto.Id))
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


            return View(imageDto);
        }

        // GET: Image/Delete/4
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var image = await _context.Image.FirstOrDefaultAsync(m => m.Id == id);
            if (image == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<ImageDTO>(image));
        }

        // POST: Image/Delete/3
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var image = await _context.Image.FindAsync(id);
            _context.Image.Remove(image);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImageExists(int id)
        {
            return _context.Image.Any(e => e.Id == id);
        }
    }
}