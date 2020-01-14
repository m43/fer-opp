using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;


        public ManageUsersController(RudesDatabaseContext context, IMapper mapper, UserManager<User> manager, RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager,
    ILogger<RegisterModel> logger,
    IEmailSender emailSender)
        {
            _context = context;
            _mapper = mapper;
            _manager = manager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
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
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,Password,ConfirmPassword,FirstName,LastName,Role")]
            UserDTO userDTO)
        {

            var x = new RegisterModel(_manager, _signInManager, _logger, _emailSender, _context);
            x.Input = new RegisterModel.InputModel()
            {
                Email = userDTO.Email,
                Password = userDTO.Password,
                ConfirmPassword = userDTO.ConfirmPassword,
                Name = userDTO.FirstName,
                LastName = userDTO.LastName
            };
            return await x.OnPostAsync();

        }  */

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
        public async Task<IActionResult> Edit(string id, [Bind("Id,Email,Password,ConfirmPassword,FirstName,LastName,Role")] UserDTO userDTO)
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
                    var roleName = userDTO.Role;
                    var roleCheck = await _roleManager.RoleExistsAsync(roleName);
                    if (!roleCheck)
                    {
                        return BadRequest();
                    }
                    var roles = await _manager.GetRolesAsync(user);
                    foreach (var role in roles)
                    {
                        await _manager.RemoveFromRoleAsync(user, role);
                    }
                    await _manager.AddToRoleAsync(user, roleName);
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

            ViewBag.Images = new SelectList(Roles.AllRoles);
        }

        private bool UserExists(string id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}


