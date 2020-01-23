using Microsoft.EntityFrameworkCore;
using RudesWebapp.Data;
using RudesWebapp.Dtos;
using RudesWebapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace RudesWebapp.Services
{
    public class ArticleInStoreService
    {
        private readonly IMapper _mapper;

        public ArticleInStoreService(IMapper mapper)
        {
            _mapper = mapper;
        }

        private ArticleInStoreDTO CreateArticleInStore(Article article)
        {
            var availability = article.ArticleAvailability;
            return new ArticleInStoreDTO
            {
                Id = article.Id,
                Name = article.Name,
                Description = article.Description,
                Type = article.Type,
                Price = article.Price,
                ArticleColor = article.ArticleColor,
                ImageId = article.ImageId,
                Image = _mapper.Map<ImageDTO>(article.Image),
                Sizes = availability
                    .Select(a => a.Size)
                    .ToList(),
                Quantities = availability
                    .Select(a => a.Quantity ?? -1)
                    .ToList()
            };
        }

        public async Task<ArticleInStoreDTO> CreateArticleInStore(RudesDatabaseContext context, int articleId)
        {
            var article = await context.Article
                .Include(a=>a.Image)
                .Include(a => a.ArticleAvailability)
                .Where(a => a.Id == articleId)
                .FirstOrDefaultAsync();

            return CreateArticleInStore(article);
        }

        public IEnumerable<ArticleInStoreDTO> CreateArticlesInStore(RudesDatabaseContext context)
        {
            var result = context.Article
                .Include(a=>a.Image)
                .Include(a => a.ArticleAvailability)
                .Select(CreateArticleInStore);

            var removeList = new List<ArticleInStoreDTO>();
            foreach (var article in result)
            {
                if (article.Quantities.Sum() == 0)
                {
                    removeList.Add(article);
                }
            }

            foreach (var removeArticle in removeList)
            {
                result = result.Where(a => a.Id != removeArticle.Id).ToList();
            }

            return result;
        }
    }
}
