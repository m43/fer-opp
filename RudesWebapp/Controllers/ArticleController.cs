using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RudesWebapp.Dtos;
using RudesWebapp.Helpers;
using RudesWebapp.Models;
using RudesWebapp.Services;

namespace RudesWebapp.Controllers
{
    [Authorize(Roles = Roles.BoardOrAbove)]
    public class ArticleController : Controller
    {
        private readonly ArticleService _articleService;
        private readonly ImageService _imageService;

        public ArticleController(ArticleService articleService, ImageService imageService)
        {
            _articleService = articleService;
            _imageService = imageService;
        }

        // GET: Article
        public async Task<IActionResult> Index()
        {
            return View(await _articleService.GetArticles());
        }

        // GET: Article/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tResult = await _articleService.GetArticle(id);
            if (!tResult.Succeeded)
            {
                return ObjectResultHelper.FromServiceResult(tResult);
            }

            return View(tResult.Value);
        }

        // GET: Article/Create
        public async Task<IActionResult> Create()
        {
            await PrepareDropDowns();
            return View();
        }

        // POST: Article/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Type,Price,Color,ImageId")]
            ArticleDTO articleDto)
        {
            if (!ModelState.IsValid)
            {
                await PrepareDropDowns();
                return View(articleDto);
            }

            if (articleDto.Id != 0)
            {
                ModelState.AddModelError(nameof(ArticleDTO.Id),
                    "Id should not be provided on create. Found id: " + articleDto.Id);
                await PrepareDropDowns();
                return View(articleDto);
            }

            var result = await _articleService.CreateArticle(articleDto);
            if (result.Succeeded) return RedirectToAction(nameof(Index));

            result.FillModelState(ModelState);
            await PrepareDropDowns();
            return View(articleDto);
        }

        // GET: Article/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tResult = await _articleService.GetArticle(id);
            if (!tResult.Succeeded)
            {
                return ObjectResultHelper.FromServiceResult(tResult);
            }

            await PrepareDropDowns();
            return View(tResult.Value);
        }

        // POST: Article/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Type,Price,Color,ImageId")]
            ArticleDTO articleDto)
        {
            if (id != articleDto.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                await PrepareDropDowns();
                return View(articleDto);
            }

            var tResult = await _articleService.PutArticle(articleDto);
            if (tResult.Succeeded) return RedirectToAction(nameof(Index));

            tResult.FillModelState(ModelState);
            await PrepareDropDowns();
            return View(articleDto);
        }

        // GET: Article/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tResult = await _articleService.GetArticle(id);
            if (!tResult.Succeeded)
            {
                return ObjectResultHelper.FromServiceResult(tResult);
            }

            return View(tResult.Value);
        }

        // POST: Article/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _articleService.DeleteIfExists(id);
            return RedirectToAction(nameof(Index));
        }


        private async Task PrepareDropDowns()
        {
            // TODO add suggested article types based on other types existing in the db

            var images = await _imageService.GetImages();
            ViewBag.Images = new SelectList(images, nameof(Image.Id), nameof(Image.Title));
        }
    }
}