using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using RudesWebapp.Data;
using RudesWebapp.Models;

namespace RudesWebapp.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private RudesDatabaseContext _context;

        public ConfirmEmailModel(UserManager<User> userManager, RudesDatabaseContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            StatusMessage = result.Succeeded ? "Thank you for confirming your email." : "Error confirming your email.";

            if (result.Succeeded)
            {
                var shoppingCart = new ShoppingCart
                {
                    User = user
                };
                _context.ShoppingCart.Add(shoppingCart);
                //user.ShoppingCart.Add(shoppingCart);
                _context.User.Find(user.Id).ShoppingCart = shoppingCart;
                _context.SaveChanges();
            }

            return Page();
        }
    }
}
