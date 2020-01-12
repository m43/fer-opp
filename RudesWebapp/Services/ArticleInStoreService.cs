using Microsoft.EntityFrameworkCore;
using RudesWebapp.Data;
using RudesWebapp.Dtos;
using RudesWebapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RudesWebapp.Services
{
    public class ArticleInStoreService
    {
        private static ArticleInStoreDTO CreateArticleInStore(Article article)
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
                Sizes = availability
                    .Select(a => a.Size)
                    .ToList(),
                Quantities = availability
                    .Select(a => a.Quantity ?? -1)
                    .ToList()
            };
        }

        public async static Task<ArticleInStoreDTO> CreateArticleInStore(RudesDatabaseContext context, int articleId)
        {
            var article = await context.Article
                .Include(a => a.ArticleAvailability)
                .Where(a => a.Id == articleId)
                .FirstOrDefaultAsync();

            return CreateArticleInStore(article);
        }

        public async static Task<IEnumerable<ArticleInStoreDTO>> CreateArticlesInStore(RudesDatabaseContext context)
        {
            return context.Article.Include(a => a.ArticleAvailability).Select(CreateArticleInStore);
        }
    }
}
