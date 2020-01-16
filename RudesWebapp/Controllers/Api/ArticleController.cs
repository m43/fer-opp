using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RudesWebapp.Dtos;
using RudesWebapp.Helpers;
using RudesWebapp.Services;

namespace RudesWebapp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly ArticleService _articleService;

        public ArticleController(ArticleService articleService)
        {
            _articleService = articleService;
        }

        // GET: api/Article
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticleDTO>>> GetArticle()
        {
            return Ok(await _articleService.GetArticles());
        }

        // GET: api/Article/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleDTO>> GetArticle(int? id)
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

            return tResult.Value;
        }
    }
}