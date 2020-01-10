using RudesWebapp.Data;
using RudesWebapp.Dtos;
using RudesWebapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RudesWebapp.Services
{
    public class ItemService
    {
        public static ItemDTO CreateItem(RudesDatabaseContext context, ShoppingCartArticle shoppingCartArticle, Article article)
        {
            var discountPercentage = context.Discount
                .Where(discount => discount.ArticleId == shoppingCartArticle.ArticleId)
                .GroupBy(discount => discount.Percentage)
                .Max()
                .Key;
            
            ItemDTO item = new ItemDTO
            {
                ShoppingCartId = shoppingCartArticle.ShoppingCartId,
                ArticleId = shoppingCartArticle.ArticleId,
                Quantity = shoppingCartArticle.Quantity,
                Size = shoppingCartArticle.Size,
                Type = article.Type,
                Price = article.Price,
                Name = article.Name,
                Description = article.Description,
                ImageId = article.ImageId,
                Argb = article.Argb,
                ArticleColor = article.ArticleColor,
                Percentage = discountPercentage
            };

            return item;
        }
    }
}
