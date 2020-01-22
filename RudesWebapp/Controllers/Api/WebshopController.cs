using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RudesWebapp.Data;
using RudesWebapp.Dtos;
using RudesWebapp.Helpers;
using RudesWebapp.Models;
using RudesWebapp.Services;

namespace RudesWebapp.Controllers.Api
{
    public class WebshopController : ControllerBase
    {
        private RudesDatabaseContext _context;

        public WebshopController(RudesDatabaseContext context)
        {
            _context = context;
        }

        // Item
        [HttpGet]
        public ActionResult<IEnumerable<ArticleInStoreDTO>> GetArticlesInStore()
        {
            try
            {
                return Ok(ArticleInStoreService.CreateArticlesInStore(_context));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<ActionResult<ArticleInStoreDTO>> GetArticleInStore(int id)
        {
            try
            {
                return Ok(await ArticleInStoreService.CreateArticleInStore(_context, id));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Authorize(Roles = Roles.UserOrAbove)]
        public async Task<IEnumerable<ItemDTO>> GetCurrentShoppingCartArticles()
        {
            // TODO if not logged in, then use cookies!

            var shoppingCart = await ShoppingCartService.GetCurrentShoppingCart(_context, User.GetUserId());

            var shoppingCartArticles = shoppingCart.ShoppingCartArticle;
            var items = new List<ItemDTO>();

            foreach (var shoppingCartArticle in shoppingCartArticles)
            {
                var availability = await _context.ArticleAvailability
                    .FindAsync(shoppingCartArticle.ArticleId, shoppingCartArticle.Size);
                if (availability != null)
                {
                    if (shoppingCartArticle.Quantity > availability.Quantity)
                    {
                        _context.ShoppingCartArticle.Remove(shoppingCartArticle);
                        await _context.SaveChangesAsync();
                        continue;
                    }
                }
                else
                {
                    _context.ShoppingCartArticle.Remove(shoppingCartArticle);
                    await _context.SaveChangesAsync();
                    continue;
                }

                var article = await _context.Article
                    .FirstOrDefaultAsync(a => a.Id == shoppingCartArticle.ArticleId);

                items.Add(new ItemDTO
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
                    ArticleColor = article.ArticleColor
                });
            }

            return items;
        }

        [HttpPost]
        [Authorize(Roles = Roles.UserOrAbove)]
        public async Task<ActionResult<ItemDTO>> AddToShoppingCart(int articleId, int quantity, string size)
        {
            // TODO if not logged in, then use cookies!

            // TODO remove quantity parameter from all function calls
            // TODO Well, actually quantity is needed and frontend should use it (F.)
            // TODO check if validation of received parameters works as expected

            if (quantity <= 0)
            {
                return null;
            }

            var shoppingCart = await ShoppingCartService.GetCurrentShoppingCart(_context, User.GetUserId());
            var selectedArticle = await _context.Article.FindAsync(articleId);

            if (selectedArticle != null)
            {
                var availability = await _context.ArticleAvailability
                    .FindAsync(articleId, size);

                if (availability != null)
                {
                    var shoppingCartArticle = await _context.ShoppingCartArticle
                        .FindAsync(shoppingCart.Id, articleId);
                    
                    int currentQuantity = 0;
                    if (shoppingCartArticle != null)
                    {
                        currentQuantity = shoppingCartArticle.Quantity;
                    }
                    
                    if (currentQuantity + quantity > availability.Quantity)
                    {
                        return NoContent(); // TODO change to something more appropriate
                    }
                }
                else
                {
                    return NoContent(); // TODO change to something more appropriate
                }

                var services = new ShoppingCartService(shoppingCart);
                services.AddArticle(_context, selectedArticle, size, quantity);

                var resultArticle = await _context.ShoppingCartArticle
                    .FirstOrDefaultAsync(cart => cart.ShoppingCartId == shoppingCart.Id
                                                 && cart.ArticleId == selectedArticle.Id);

                return Ok(ItemService.CreateItem(_context, resultArticle, selectedArticle));
            }

            return NotFound();
        }

        [HttpDelete]
        [Authorize(Roles = Roles.UserOrAbove)]
        public async Task<ActionResult<ItemDTO>> RemoveFromShoppingCart(int articleId, int quantity, string size)
        {
            // TODO if not logged in, then use cookies!

            var shoppingCart = await ShoppingCartService.GetCurrentShoppingCart(_context, User.GetUserId());

            var selectedArticle = await _context.Article.FindAsync(articleId);
            if (selectedArticle == null) return NotFound();

            var service = new ShoppingCartService(shoppingCart);
            var removedArticle = service.RemoveArticle(_context, selectedArticle, quantity, size);

            return Ok(ItemService.CreateItem(_context, removedArticle, selectedArticle));
        }


        [HttpGet]
        [Authorize(Roles = Roles.UserOrAbove)]
        public async Task<ActionResult<int>> ClearShoppingCart()
        {
            // TODO if not logged in, then use cookies!

            var shoppingCart = await ShoppingCartService.GetCurrentShoppingCart(_context, User.GetUserId());
            var service = new ShoppingCartService(shoppingCart);
            await service.ClearShoppingCart(_context);

            return 0;
        }

        [HttpGet]
        [Authorize(Roles = Roles.UserOrAbove)]
        public async Task<ActionResult<decimal>> GetTotalPrice()
        {
            var shoppingCart = await ShoppingCartService.GetCurrentShoppingCart(_context, User.GetUserId());
            var service = new ShoppingCartService(shoppingCart);
            return await service.GetShoppingCartTotal(_context);
        }
    }
}