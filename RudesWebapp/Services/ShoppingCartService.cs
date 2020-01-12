using Microsoft.EntityFrameworkCore;
using RudesWebapp.Data;
using RudesWebapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace RudesWebapp.Services
{
    public class ShoppingCartService
    {
        private ShoppingCart _shoppingCart;
        public ShoppingCartService() { }
        public ShoppingCartService(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        public static async Task<ShoppingCart> GetCurrentShoppingCart(RudesDatabaseContext context, string userId)
        {
            var shoppingCart = await context.ShoppingCart
                .Include(cart => cart.ShoppingCartArticle)
                .FirstAsync(cart => cart.UserId == userId);
            return shoppingCart;
        }

        public void AddArticle(RudesDatabaseContext context, Article article, string size)
        {
            var shoppingCartArticle = context.ShoppingCartArticle
                .SingleOrDefault(s => s.ArticleId == article.Id && s.ShoppingCartId == _shoppingCart.Id);

            if (shoppingCartArticle == null)
            {
                shoppingCartArticle = new ShoppingCartArticle
                {
                    Article = article,
                    ShoppingCartId = _shoppingCart.Id,
                    Quantity = 1,
                    Size = size
                };

                context.ShoppingCartArticle.Add(shoppingCartArticle);
            }
            else
            {
                shoppingCartArticle.Quantity++;
            }

            context.SaveChanges();
        }

        public ShoppingCartArticle RemoveArticle(RudesDatabaseContext context, Article article, int quantity,
            string size)
        {
            // TODO localQuantity, quantity and size are never user

            ShoppingCartArticle shoppingCartArticle = context.ShoppingCartArticle
                .SingleOrDefault(s => s.ArticleId == article.Id && s.ShoppingCartId == _shoppingCart.Id);

            var localQuantity = 0;

            if (shoppingCartArticle != null)
            {
                if (shoppingCartArticle.Quantity > 1)
                {
                    shoppingCartArticle.Quantity--;
                    localQuantity = shoppingCartArticle.Quantity;
                }
                else
                {
                    context.ShoppingCartArticle.Remove(shoppingCartArticle);
                }
            }

            context.SaveChanges();

            return shoppingCartArticle;
        }

        public async Task ClearShoppingCart(RudesDatabaseContext context)
        {
            await context.ShoppingCartArticle
                .Where(cart => cart.ShoppingCartId == _shoppingCart.Id)
                .DeleteAsync();
        }

        public async Task<decimal> GetShoppingCartTotal(RudesDatabaseContext context)
        {
            return await context.ShoppingCartArticle
                .Where(c => c.ShoppingCartId == _shoppingCart.Id)
                .Select(c => c.Article.Price * c.Quantity * (100 - c.Article.Discount.Max(x => x.Percentage)) / 100)
                .SumAsync();

            // TODO is some kind of .Include(c=>c.Discount) needed?
            // TODO how to handle multiple dicounts active?
        }
    }
}
