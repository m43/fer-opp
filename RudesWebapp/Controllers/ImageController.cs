using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RudesWebapp.Dtos;
using RudesWebapp.Models;
using RudesWebapp.Services;

namespace RudesWebapp.Controllers
{
    [Authorize(Roles = Roles.CoachOrAbove)]
    public class ImageController : Controller
    {
        private readonly ImageService _imageService;
        private readonly IMapper _mapper;

        public ImageController(ImageService imageService, IMapper mapper)
        {
            _imageService = imageService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _imageService.GetImages());
        }


        [Authorize(Roles = Roles.CoachOrAbove)]
        public async Task<IActionResult> Scan()
        {
            await _imageService.ScanImages();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var image = await _imageService.GetFirstImageOrDefault(id);
            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }

        // GET: Image/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Caption,AltText,Picture")]
            AddImageDTO imageDto)
        {
            if (!ModelState.IsValid)
            {
                return View(imageDto);
            }

            if (imageDto.Picture == null)
            {
                ModelState.AddModelError(nameof(imageDto.Picture), "Its necessary to provide an picture");
                return View(imageDto);
            }

            var image = await _imageService.SaveImage(imageDto);
            return RedirectToAction(nameof(Details), new {id = image.Id});
        }

        // GET: Image/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var image = await _imageService.GetImage(id);
            if (image == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<AddImageDTO>(image));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Caption,AltText,Picture")]
            AddImageDTO imageDto)
        {
            if (id != imageDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var image = await _imageService.Update(id, imageDto);
                if (image == null)
                {
                    return NotFound();
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

            var image = await _imageService.GetFirstImageOrDefault(id);
            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }

        // POST: Image/Delete/3
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _imageService.DeleteImage(id);
            return RedirectToAction(nameof(Index));
        }
    }
}