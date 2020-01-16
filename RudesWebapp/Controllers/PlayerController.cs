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
using RudesWebapp.Services;

namespace RudesWebapp.Controllers
{
    [Authorize(Roles = Roles.CoachOrAbove)]
    public class PlayerController : Controller
    {
        private readonly RudesDatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly ImageService _imageService;

        public PlayerController(RudesDatabaseContext context, IMapper mapper, ImageService imageService)
        {
            _context = context;
            _mapper = mapper;
            _imageService = imageService;
        }

        // GET: Player
        public async Task<IActionResult> Index()
        {
            return View(await _context.Player.Include(p => p.Image).ToListAsync());
        }

        // GET: Player/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Player.Include(p => p.Image).FirstOrDefaultAsync(m => m.Id == id);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // GET: Player/Create
        public async Task<IActionResult> Create()
        {
            await PrepareDropDowns();
            return View();
        }

        // POST: Player/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,LastName,BirthDate,PlayerType,Position,ImageId")]
            PlayerDTO playerDto)
        {
            if (playerDto.Id != 0)
            {
                ModelState.AddModelError(nameof(ArticleDTO.Id),
                    "Id should not be provided on create. Found id: " + playerDto.Id);
                await PrepareDropDowns();
                return View(playerDto);
            }

            if (ModelState.IsValid)
            {
                if (playerDto.ImageId != null)
                {
                    var result = await _imageService.ValidateThatImageExists(nameof(PlayerDTO.ImageId), playerDto.ImageId.Value);
                    if (!result.Succeeded)
                    {
                        result.FillModelState(ModelState);
                        await PrepareDropDowns();
                        return View(playerDto);
                    }
                }

                _context.Add((object) _mapper.Map<Player>(playerDto));
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            await PrepareDropDowns();
            return View(playerDto);
        }

        // GET: Player/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Player.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }

            await PrepareDropDowns();
            return View(_mapper.Map<PlayerDTO>(player));
        }

        // POST: Player/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,LastName,BirthDate,PlayerType,Position,ImageId")]
            PlayerDTO playerDto)
        {
            if (id != playerDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (playerDto.ImageId != null)
                {
                    var result = await _imageService.ValidateThatImageExists(nameof(PlayerDTO.ImageId), playerDto.ImageId.Value);
                    if (!result.Succeeded)
                    {
                        result.FillModelState(ModelState);
                        await PrepareDropDowns();
                        return View(playerDto);
                    }
                }
                
                try
                {
                    // NOTE: https://stackoverflow.com/questions/13314666/using-automapper-to-update-an-existing-entity-poco/25242322
                    var player = await _context.Player.FindAsync(id);
                    _mapper.Map(playerDto, player);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerExists(playerDto.Id))
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
            return View(playerDto);
        }

        // GET: Player/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Player.Include(p => p.Image).FirstOrDefaultAsync(m => m.Id == id);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // POST: Player/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var player = await _context.Player.FindAsync(id);
            _context.Player.Remove(player);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task PrepareDropDowns()
        {
            var images = await _context.Image.ToListAsync();
            ViewBag.Images = new SelectList(images, nameof(Image.Id), nameof(Image.Title));
        }

        private bool PlayerExists(int id)
        {
            return _context.Player.Any(e => e.Id == id);
        }
    }
}