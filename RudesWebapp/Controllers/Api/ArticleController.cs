using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RudesWebapp.Data;
using RudesWebapp.Dtos;
using RudesWebapp.Filters;
using RudesWebapp.Models;

namespace RudesWebapp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly RudesDatabaseContext _context;
        private readonly IMapper _mapper;

        public ArticleController(RudesDatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Article
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticleDTO>>> GetArticle()
        {
            return _mapper.Map<List<ArticleDTO>>(await _context.Article.ToListAsync());
        }

        // GET: api/Article/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleDTO>> GetArticle(int id)
        {
            var article = await _context.Article.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            return _mapper.Map<ArticleDTO>(article);
        }

        // PUT: api/Article/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        [Authorize(Roles = Roles.BoardOrAbove)]
        [ValidateModel]
        public async Task<IActionResult> PutArticle(int id, ArticleDTO articleDto)
        {
            if (id != articleDto.Id)
            {
                return BadRequest();
            }

            // TODO not sure if this is good
            try
            {
                var article = await _context.Article.FindAsync(id);
                if (article == null)
                {
                    return NotFound();
                }

                _mapper.Map(articleDto, article);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(articleDto.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Article
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = Roles.BoardOrAbove)]
        public async Task<ActionResult<ArticleDTO>> PostArticle(ArticleDTO articleDto)
        {
            // TODO is check for id == 0 necessary?

            var article = _mapper.Map<Article>(articleDto);
            _context.Article.Add(article);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArticle", new {id = articleDto.Id}, articleDto);
        }

        // DELETE: api/Article/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ArticleDTO>> DeleteArticle(int id)
        {
            var article = await _context.Article.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            _context.Article.Remove(article);
            await _context.SaveChangesAsync();

            return _mapper.Map<ArticleDTO>(article);
        }

        private bool ArticleExists(int id)
        {
            return _context.Article.Any(e => e.Id == id);
        }
    }
}