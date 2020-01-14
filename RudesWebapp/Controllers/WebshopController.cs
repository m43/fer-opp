using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RudesWebapp.Data;
using RudesWebapp.Dtos;
using RudesWebapp.Services;
using RudesWebapp.Models;
using RudesWebapp.Helpers;
using System;

namespace RudesWebapp.Controllers
{
    public class WebshopController : Controller
    {
        private RudesDatabaseContext _context;
        private UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public WebshopController(RudesDatabaseContext context, UserManager<User> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }

        public IActionResult WebshopStart()
        {
            return View();
        }

        // Item
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticleInStoreDTO>>> GetArticlesInStore()
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

        // Article
        // !! Povezati sa ArticleAvailability (ne vlastite metode, vec unutar ovih od Article-a)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticles()
        {
            return await _context.Article.ToListAsync();
        }

        [HttpGet]
        public async Task<ActionResult<Article>> GetArticle(int id)
        {
            var article = await _context.Article.FindAsync(id);

            if (article == null)
            {
                return NotFound();
            }

            return article;
        }

        [HttpPost]
        [Authorize(Roles = Roles.BoardOrAbove)]
        public async Task<ActionResult<Article>> AddArticle(Article article)
        {
            _context.Article.Add(article);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArticle", new {id = article.Id}, article);
        }

        [HttpPut]
        [Authorize(Roles = Roles.BoardOrAbove)]
        public async Task<ActionResult<Article>> UpdateArticle(int id, Article article)
        {
            if (id != article.Id)
            {
                return BadRequest();
            }

            _context.Entry(article).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var article_from_database = await _context.Article.FindAsync(id);
                if (article_from_database == null)
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

        [HttpDelete]
        [Authorize(Roles = Roles.BoardOrAbove)]
        public async Task<ActionResult<Article>> RemoveArticle(int id)
        {
            var article = await _context.Article.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            _context.Article.Remove(article);
            await _context.SaveChangesAsync();

            return article;
        }

        // Image

        [HttpGet]
        [Authorize(Roles = Roles.UserOrAbove)]
        public async Task<ActionResult<IEnumerable<Image>>> GetImages()
        {
            return await _context.Image.ToListAsync();
        }

        [HttpGet]
        [Authorize(Roles = Roles.UserOrAbove)]
        public async Task<ActionResult<Image>> GetImage(int id)
        {
            var image = await _context.Image.FindAsync(id);

            if (image == null)
            {
                return NotFound();
            }

            return image;
        }

        [HttpPost]
        [Authorize(Roles = Roles.BoardOrAbove)]
        public async Task<ActionResult<Image>> UploadImage(Image image)
        {
            _context.Image.Add(image);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetImage", new {id = image.Id}, image);
        }

        [HttpDelete]
        [Authorize(Roles = Roles.BoardOrAbove)]
        public async Task<ActionResult<Image>> DeleteImage(int id)
        {
            var image = await _context.Image.FindAsync(id);
            if (image == null)
            {
                return NotFound();
            }

            _context.Image.Remove(image);
            await _context.SaveChangesAsync();

            return image;
        }

        // Discount

        [HttpGet]
        [Authorize(Roles = Roles.UserOrAbove)]
        public async Task<ActionResult<IEnumerable<Discount>>> GetDiscounts()
        {
            return await _context.Discount.ToListAsync();
        }

        [HttpGet]
        [Authorize(Roles = Roles.UserOrAbove)]
        public async Task<ActionResult<Discount>> GetDiscount(int id)
        {
            var discount = await _context.Discount.FindAsync(id);

            if (discount == null)
            {
                return NotFound();
            }

            return discount;
        }

        [HttpPost]
        [Authorize(Roles = Roles.BoardOrAbove)]
        public async Task<ActionResult<Discount>> AddDiscount(Discount discount)
        {
            _context.Discount.Add(discount);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDiscount", new {id = discount.Id}, discount);
        }

        [HttpPut]
        [Authorize(Roles = Roles.BoardOrAbove)]
        public async Task<ActionResult<Discount>> UpdateDiscount(int id, Discount discount)
        {
            if (id != discount.Id)
            {
                return BadRequest();
            }

            _context.Entry(discount).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var discountFromDatabase = _context.Discount.FindAsync(id);
                if (discountFromDatabase == null) // TODO expression is always false!
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

        [HttpDelete]
        [Authorize(Roles = Roles.BoardOrAbove)]
        public async Task<ActionResult<Discount>> RemoveDiscount(int id)
        {
            var discount = await _context.Discount.FindAsync(id);
            if (discount == null)
            {
                return NotFound();
            }

            _context.Discount.Remove(discount);
            await _context.SaveChangesAsync();

            return discount;
        }

        // Review

        [HttpGet]
        [Authorize(Roles = Roles.UserOrAbove)]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviews()
        {
            return await _context.Review.ToListAsync();
        }

        [HttpGet]
        [Authorize(Roles = Roles.UserOrAbove)]
        public async Task<ActionResult<Review>> GetReview(int id)
        {
            var review = await _context.Review.FindAsync(id);

            if (review == null)
            {
                return NotFound();
            }

            return review;
        }

        [HttpPost]
        [Authorize(Roles = Roles.UserOrAbove)]
        public async Task<ActionResult<Review>> AddReview(Review review)
        {
            _context.Review.Add(review);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReview", new {id = review.Id}, review);
        }

        [HttpPut]
        [Authorize(Roles = Roles.UserOrAbove)]
        public async Task<ActionResult<Review>> UpdateReview(int id, Review review)
        {
            if (id != review.Id)
            {
                return BadRequest();
            }

            _context.Entry(review).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var reviewFromDatabase = await _context.Review.FindAsync(id);
                if (reviewFromDatabase == null)
                {
                    return NotFound();
                }
                
                throw;
            }

            return NoContent();
        }

        [HttpDelete]
        [Authorize(Roles = Roles.UserOrAbove)]
        public async Task<ActionResult<Review>> DeleteReview(int id)
        {
            var review = await _context.Review.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            _context.Review.Remove(review);
            await _context.SaveChangesAsync();

            return review;
        }

        // Shopping Cart
        [HttpGet]
        [Authorize(Roles = Roles.UserOrAbove)]
        public async Task<IEnumerable<ItemDTO>> GetCurrentShoppingCartArticles()
        {
            // TODO if not logged in, then use cookies!

            var shoppingCart = await ShoppingCartService.GetCurrentShoppingCart(_context, User.GetUserId());

            var shoppingCartArticles = shoppingCart.ShoppingCartArticle;
            var items = new List<ItemDTO>();

            foreach (ShoppingCartArticle shoppingCartArticle in shoppingCartArticles)
            {
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
            var shoppingCart = await ShoppingCartService.GetCurrentShoppingCart(_context, User.GetUserId());
            var selectedArticle = await _context.Article.FindAsync(articleId);

            if (selectedArticle != null)
            {
                /*
                var availability = await _context.ArticleAvailability.FindAsync(articleId, size);
                if (availability != null)
                {
                    if (quantity > availability.Quantity)
                    {
                        return NotFound();
                    }
                }
                */

                ShoppingCartService services = new ShoppingCartService(shoppingCart);
                services.AddArticle(_context, selectedArticle, size);
                
                var resultArticle = await _context.ShoppingCartArticle
                    .FirstOrDefaultAsync(cart => cart.ShoppingCartId == shoppingCart.Id
                                                 && cart.ArticleId == selectedArticle.Id);

                /*
                availability.Quantity -= quantity;
                await _context.SaveChangesAsync();
                */

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
            if (selectedArticle != null)
            {
                ShoppingCartService service = new ShoppingCartService(shoppingCart);
                var removedArticle = service.RemoveArticle(_context, selectedArticle, quantity, size);

                return Ok(ItemService.CreateItem(_context, removedArticle, selectedArticle));
            }

            return NotFound();
        }


        [HttpGet]
        [Authorize(Roles = Roles.UserOrAbove)]
        public async Task<ActionResult<int>> ClearShoppingCart()
        {
            // TODO if not logged in, then use cookies!

            var shoppingCart = await ShoppingCartService.GetCurrentShoppingCart(_context, User.GetUserId());
            ShoppingCartService service = new ShoppingCartService(shoppingCart);
            await service.ClearShoppingCart(_context);

            return 0;
        }

        [HttpGet]
        [Authorize(Roles = Roles.UserOrAbove)]
        public async Task<ActionResult<decimal>> GetTotalPrice()
        {
            var shoppingCart = await ShoppingCartService.GetCurrentShoppingCart(_context, User.GetUserId());
            ShoppingCartService service = new ShoppingCartService(shoppingCart);
            return await service.GetShoppingCartTotal(_context);
        }


        // Order

        [HttpGet]
        [Authorize(Roles = Roles.UserOrAbove)]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Order.ToListAsync();
        }

        [HttpGet]
        [Authorize(Roles = Roles.UserOrAbove)]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Order.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        [HttpPost]
        [Authorize(Roles = Roles.UserOrAbove)]
        public async Task<ActionResult<Order>> CreateOrder(Order order)
        {
            _context.Order.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new {id = order.Id}, order);
        }

        [HttpPut]
        [Authorize(Roles = Roles.UserOrAbove)]
        public async Task<ActionResult<Order>> UpdateOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var order_from_database = await _context.Order.FindAsync(id);
                if (order_from_database == null)
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

        [HttpDelete]
        [Authorize(Roles = Roles.UserOrAbove)]
        public async Task<ActionResult<Order>> DeleteOrder(int id)
        {
            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Order.Remove(order);
            await _context.SaveChangesAsync();

            return order;
        }

        [HttpPost]
        [Authorize(Roles = Roles.UserOrAbove)]
        public async Task BuyArticle(int articleId)
        {
            // Use article model CRUD methods defined in this controller
        }

        private async Task<ActionResult<User>> GetCurrentUserAsync()
        {
            var user = await _context.Users.FindAsync(User.GetUserId());
            // TODO map to UserDTO and return. Its not smart to return the user..
            return user;
        }

        [HttpGet]
        public ActionResult<User> GetCurrentUser()
        {
            return GetCurrentUserAsync().Result;
        }
    }
}