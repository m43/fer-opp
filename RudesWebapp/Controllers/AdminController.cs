using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RudesWebapp.Data;
using RudesWebapp.Models;

namespace RudesWebapp.Controllers
{
    [Authorize(Roles = Roles.AdminOnly)]
    public class AdminController : Controller
    {
        private readonly RudesDatabaseContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public AdminController(RudesDatabaseContext context, RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        // User

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpGet]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPut]
        public async Task<ActionResult<User>> UpdateUser(string id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var userFromDatabase = await _context.User.FindAsync(id);
                if (userFromDatabase == null)
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
        public async Task<ActionResult<User>> DeleteUser(string id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }


        [HttpPut]
        public async Task AssignRole(User user, IdentityRole role)
        {
            // Check if the role exists
            var roleCheck = await _roleManager.RoleExistsAsync(role.Name);
            if (roleCheck == false)
            {
                return; // TODO privremeno
            }

            // Check if the user exists
            var userCheck = await _userManager.GetUserIdAsync(user);
            if (userCheck == null)
            {
                return; // TODO privremeno
            }

            // Check if the user has already been assigned the specified role
            var userRoleCheck = await _userManager.IsInRoleAsync(user, role.Name);
            if (userRoleCheck == false)
            {
                return; // TODO privremeno, dok ne napravimo nove Exception-e za nase potrebe
            }

            await _userManager.AddToRoleAsync(user, role.Name);
        }

        private async Task<User> GetCurrentUserAsync()
        {
            return await _userManager.GetUserAsync(HttpContext.User);
        }

        [HttpGet]
        public async Task<ActionResult<User>> GetCurrentUser()
        {
            return await GetCurrentUserAsync();
        }
    }
}