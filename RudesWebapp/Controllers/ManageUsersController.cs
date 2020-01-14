using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RudesWebapp.Data;
using RudesWebapp.Dtos;
using RudesWebapp.Models;

namespace RudesWebapp.Controllers
{
    [Authorize(Roles = Roles.AdminOnly)]
    public class ManageUsersController : Controller
    {
        private readonly RudesDatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _manager;


        public ManageUsersController(RudesDatabaseContext context, IMapper mapper, UserManager<User> manager)
        {
            _context = context;
            _mapper = mapper;
            _manager = manager;
        }

        // GET: ManageUsers
        public async Task<IActionResult> Index()
        {

            var userList = await _context.User.ToListAsync();
            var result = new List<UserDTO>();
            foreach (var user in userList)
            {
                if (user == null)
                {
                    continue;
                }
                result.Add(new UserDTO
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.Name,
                    LastName = user.LastName,
                    Password = user.PasswordHash,
                    ConfirmPassword = user.PasswordHash,
                    Role = (await _manager.GetRolesAsync(user)).First()
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
            var userDTO = new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                LastName = user.LastName,
                FirstName = user.Name,
                Password = user.PasswordHash,
                ConfirmPassword = user.PasswordHash,
                Role = (await _manager.GetRolesAsync(user)).First()

            };

            return View(userDTO);
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
        /* [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Email,Password,ConfirmPassword,FirstName,LastName,Role")]
            UserDTO userDTO)
        { 

            if (!ModelState.IsValid) return Page();
            var user = new User
            { UserName = userDTO.FirstName + userDTO.LastName, Email = userDTO.Email, Name = userDTO.FirstName, LastName = userDTO.LastName };
            var result = await _manager.CreateAsync(user, userDTO.Password);
            if (result.Succeeded)
            {

                // Adding a shopping cart for the new user
                var shoppingCart = new ShoppingCart
                {
                    User = user
                };
                _context.ShoppingCart.Add(shoppingCart);
                _context.User.Find(user.Id).ShoppingCart = shoppingCart;
                _context.SaveChanges();
                //_logger.LogInformation("User created a new account with password.");
                return RedirectToAction(nameof(Index));
            }

            await PrepareDropDowns();
            return View(userDTO);
        } */

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
            var userDTO = new UserDTO
            {
                Id = user.Id,
                FirstName = user.Name,
                LastName = user.LastName,
                Password = user.PasswordHash,
                ConfirmPassword = user.PasswordHash,
                Email = user.Email,
                Role = (await _manager.GetRolesAsync(user)).First()

            };

            PrepareDropDowns();
            return View(userDTO); 
        } 

        // POST: Article/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Role")] UserDTO userDTO)
        {
            if (id != userDTO.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _context.User.FindAsync(id);
                    _mapper.Map(userDTO, user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(userDTO.Id))
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
            return View(userDTO);
        }

        // GET: Article/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.FirstOrDefaultAsync(m => m.Id == id);
            var userDTO = new UserDTO
            {
                Id = user.Id,
                FirstName = user.Name,
                LastName = user.LastName,
                Password =  null,
                ConfirmPassword = null,
                Email = user.Email,
                Role = (await _manager.GetRolesAsync(user)).First()
            };
            if (user == null)
            {
                return NotFound();
            }

            return View(userDTO);
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
            var roles = new List<string>();
            string[] possibleRoles = { "Admin", "Board",
                        "Coach", "User" };
            roles.AddRange(possibleRoles);
            ViewBag.Images = new SelectList(roles);
        }

        private bool UserExists(string id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}


