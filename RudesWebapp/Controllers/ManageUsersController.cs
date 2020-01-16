using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RudesWebapp.Areas.Identity.Pages.Account;
using RudesWebapp.Data;
using RudesWebapp.Dtos;
using RudesWebapp.Filters;
using RudesWebapp.Helpers;
using RudesWebapp.Models;

namespace RudesWebapp.Controllers
{
    [Authorize(Roles = Roles.AdminOnly)]
    public class ManageUsersController : Controller
    {
        private readonly RudesDatabaseContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;


        public ManageUsersController(
            RudesDatabaseContext context,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        // GET: ManageUsers
        public async Task<IActionResult> Index()
        {
            var userList = await _context.User.ToListAsync();
            var result = new List<ManageUserDTO>();
            foreach (var user in userList)
            {
                if (user == null)
                {
                    continue;
                }

                result.Add(new ManageUserDTO
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.Name,
                    LastName = user.LastName,
                    Password = user.PasswordHash,
                    Role = (await _userManager.GetRolesAsync(user)).First()
                });
            }

            return View(result);
        }

        // GET: ManageUsers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            var userDto = new ManageUserDTO
            {
                Id = user.Id,
                Email = user.Email,
                LastName = user.LastName,
                FirstName = user.Name,
                Password = user.PasswordHash,
                ConfirmPassword = user.PasswordHash,
                Role = (await _userManager.GetRolesAsync(user)).First()
            };

            return View(userDto);
        }

        // GET: ManageUsers/Create
        public IActionResult Create()
        {
            PrepareDropDowns();
            return View();
        }

        // POST: Article/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateModel]
        public async Task<IActionResult> Create([Bind("Id,Email,Password,ConfirmPassword,FirstName,LastName,Role")]
            ManageUserDTO manageUserDto)
        {
            if (!await Roles.CheckRoleExists(_roleManager, manageUserDto.Role))
            {
                ModelState.AddModelError(nameof(ManageUserDTO.Role), "Given role does not exist.");
                PrepareDropDowns();
                return View(manageUserDto);
            }

            var user = new User
            {
                UserName = manageUserDto.Email, Email = manageUserDto.Email, Name = manageUserDto.FirstName,
                LastName = manageUserDto.LastName
            };

            var result = await _userManager.CreateAsync(user, manageUserDto.Password);
            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");

                await _userManager.AddToRoleAsync(user, manageUserDto.Role);

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new {area = "Identity", userId = user.Id, code = code},
                    protocol: Request.Scheme);

                await _emailSender.SendEmailAsync(manageUserDto.Email, "Confirm your email",
                    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                return RedirectToAction(nameof(Details), new {id = user.Id});
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            PrepareDropDowns();
            return View(manageUserDto);
        }

        // GET: Article/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var userDto = new UpdateUserRoleDTO
            {
                Id = user.Id,
                Role = (await _userManager.GetRolesAsync(user)).First()
            };

            PrepareDropDowns();
            return View(userDto);
        }

        // POST: Article/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Role")] UpdateUserRoleDTO updateUserRoleDto)
        {
            if (id != updateUserRoleDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _context.User.FindAsync(id);
                    if (!await user.SetRole(_userManager, _roleManager, updateUserRoleDto.Role))
                    {
                        return BadRequest();
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(updateUserRoleDto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            PrepareDropDowns();
            return View(updateUserRoleDto);
        }

        // GET: Article/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            var userDto = new ManageUserDTO
            {
                Id = user.Id,
                FirstName = user.Name,
                LastName = user.LastName,
                Password = null,
                ConfirmPassword = null,
                Email = user.Email,
                Role = (await _userManager.GetRolesAsync(user)).First()
            };

            return View(userDto);
        }

        // POST: ManageUsers/Delete/abc
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _context.User.FindAsync(id);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private void PrepareDropDowns()
        {
            ViewBag.Roles = new SelectList(Roles.AllRoles);
        }

        private bool UserExists(string id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}