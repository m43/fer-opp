using RudesWebapp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RudesWebapp.Models
{
    public partial class ShoppingCart : IDateCreatedAndUpdated
    {
        public ShoppingCart()
        {
            ShoppingCartArticle = new HashSet<ShoppingCartArticle>();
        }

        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? LastModificationDate { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<ShoppingCartArticle> ShoppingCartArticle { get; set; }

        public static ShoppingCart GetCurrentShoppingCart(RudesDatabaseContext context, User user)
        {
            var shoppingCart = context.ShoppingCart.
                                FirstOrDefault(cart => cart.UserId == user.Id);

            var currentShoppingCart = context.ShoppingCart.Find(shoppingCart.Id);
            currentShoppingCart.User = null;

            // TODO
            if (currentShoppingCart == null)
            {
                return null;
            }

            return currentShoppingCart;
        }

        public void AddArticle(RudesDatabaseContext context, Article article, int quantity, string size)
        {
            ShoppingCartArticle shoppingCartArticle = context.ShoppingCartArticle
                .SingleOrDefault(s => s.ArticleId == article.Id && s.ShoppingCartId == Id);

            if (shoppingCartArticle == null)
            {
                shoppingCartArticle = new ShoppingCartArticle
                {
                    Article = article,
                    ShoppingCartId = Id,
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

        public ShoppingCartArticle RemoveArticle(RudesDatabaseContext context, Article article, int quantity, string size)
        {
            ShoppingCartArticle shoppingCartArticle = context.ShoppingCartArticle
                .SingleOrDefault(s => s.ArticleId == article.Id && s.ShoppingCartId == Id);

            var localQuantity = 0;

            if (shoppingCartArticle != null)
            {
                if (shoppingCartArticle.Quantity > 1)
                {
                    shoppingCartArticle.Quantity--;
                    localQuantity = shoppingCartArticle.Quantity ?? default;
                }
                else
                {
                    context.ShoppingCartArticle.Remove(shoppingCartArticle);
                }
            }

            context.SaveChanges();

            return shoppingCartArticle;
        }

        public void ClearShoppingCart(RudesDatabaseContext context)
        {
            var shoppingCartArticles = context.ShoppingCartArticle
                .Where(cart => cart.ShoppingCartId == Id);

            context.ShoppingCartArticle.RemoveRange(shoppingCartArticles);

            context.SaveChanges();
        }

        public decimal GetShoppingCartTotal(RudesDatabaseContext context)
        {
            // TODO for the sake of simplicity, Discount was not included in the total sum
            var total = context.ShoppingCartArticle.Where(c => c.ShoppingCartId == Id)
                .Select(c => c.Article.Price * c.Quantity).Sum();
            
            return total ?? default;
        }
    }
}