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
            var discountPercentage = 0;

            var discounts = context.Discount
                .Where(discount => discount.ArticleId == shoppingCartArticle.ArticleId)
                .Select(discount => discount.Percentage)
                .ToList();

            if (discounts.Count() != 0)
            {
                discountPercentage = discounts.Max();
            }

            // TODO remove Argb and ShoppingCartId
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
