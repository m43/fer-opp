using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RudesWebapp.Data;
using RudesWebapp.Dtos;

namespace RudesWebapp.Controllers
{
    public class ManageUsersController : Controller
    {
        private readonly RudesDatabaseContext _context;

        public ManageUsersController(RudesDatabaseContext context)
        {
            _context = context;
        }

        // GET: ManageUsers
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserDTO.ToListAsync());
        }

        // GET: ManageUsers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userDTO = await _context.UserDTO
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userDTO == null)
            {
                return NotFound();
            }

            return View(userDTO);
        }

        // GET: ManageUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ManageUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,Password,ConfirmPassword,FirstName,LastName,Role")] UserDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userDTO);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userDTO);
        }

        // GET: ManageUsers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userDTO = await _context.UserDTO.FindAsync(id);
            if (userDTO == null)
            {
                return NotFound();
            }
            return View(userDTO);
        }

        // POST: ManageUsers/Edit/5
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
                    _context.Update(userDTO);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserDTOExists(userDTO.Id))
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
            return View(userDTO);
        }

        // GET: ManageUsers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userDTO = await _context.UserDTO
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userDTO == null)
            {
                return NotFound();
            }

            return View(userDTO);
        }

        // POST: ManageUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var userDTO = await _context.UserDTO.FindAsync(id);
            _context.UserDTO.Remove(userDTO);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserDTOExists(string id)
        {
            return _context.UserDTO.Any(e => e.Id == id);
        }
    }
}
