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
using RudesWebapp.Services;

namespace RudesWebapp.Controllers
{
    [Authorize(Roles = Roles.CoachOrAbove)]
    public class PostController : Controller
    {
        private readonly RudesDatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly ImageService _imageService;

        public PostController(RudesDatabaseContext context, IMapper mapper, ImageService imageService)
        {
            _context = context;
            _mapper = mapper;
            _imageService = imageService;
        }


        // GET: Post
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<PostDTO>>(await _context.Post.Include(p => p.Image).ToListAsync()));
        }

        // GET: Post/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post.Include(p => p.Image).FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<PostDTO>(post));
        }

        // GET: Post/Create
        public async Task<IActionResult> Create()
        {
            await PrepareDropDowns();
            return View();
        }

        // POST: Post/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Content,PostType,StartDate,EndDate,ImageId")]
            PostDTO postDto)
        {
            if (postDto.Id != 0)
            {
                ModelState.AddModelError(nameof(ArticleDTO.Id),
                    "Id should not be provided on create. Found id: " + postDto.Id);
                await PrepareDropDowns();
                return View(postDto);
            }

            if (ModelState.IsValid)
            {
                if (postDto.ImageId != null)
                {
                    var result =
                        await _imageService.ValidateThatImageExists(nameof(PlayerDTO.ImageId), postDto.ImageId.Value);
                    if (!result.Succeeded)
                    {
                        result.FillModelState(ModelState);
                        await PrepareDropDowns();
                        return View(postDto);
                    }
                }

                _context.Add((object) _mapper.Map<Post>(postDto));
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            await PrepareDropDowns();
            return View(postDto);
        }

        // GET: Post/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            await PrepareDropDowns();
            return View(_mapper.Map<PostDTO>(post));
        }

        // POST: Post/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,PostType,StartDate,EndDate,ImageId")]
            PostDTO postDto)
        {
            if (id != postDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (postDto.ImageId != null)
                {
                    var result =
                        await _imageService.ValidateThatImageExists(nameof(PlayerDTO.ImageId), postDto.ImageId.Value);
                    if (!result.Succeeded)
                    {
                        result.FillModelState(ModelState);
                        await PrepareDropDowns();
                        return View(postDto);
                    }
                }

                try
                {
                    var post = await _context.Post.FindAsync(id);
                    _mapper.Map(postDto, post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(postDto.Id))
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
            return View(postDto);
        }

        // GET: Post/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post.Include(p => p.Image).FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<PostDTO>(post));
        }

        // POST: Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Post.FindAsync(id);
            _context.Post.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task PrepareDropDowns()
        {
            var images = await _context.Image.ToListAsync();
            ViewBag.Images = new SelectList(images, nameof(Image.Id), nameof(Image.Title));
        }

        private bool PostExists(int id)
        {
            return _context.Post.Any(e => e.Id == id);
        }
    }
}