using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RudesWebapp.Data;
using RudesWebapp.Dtos;
using RudesWebapp.Models;

namespace RudesWebapp.Controllers
{
    [Authorize(Roles = Roles.BoardOrAbove)]
    public class OrderController : Controller
    {
        private readonly RudesDatabaseContext _context;
        private readonly IMapper _mapper;

        public OrderController(RudesDatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<OrderDTO>>(await _context.Order.ToListAsync()));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<OrderDTO>(order));
        }

        // GET: Order/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,FulFilled,Address,City,PostalCode")]
            OrderDTO orderDto)
        {
            if (ModelState.IsValid)
            {
                _context.Add((object) _mapper.Map<Order>(orderDto));
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(orderDto);
        }

        // GET: Order/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }


            return View(_mapper.Map<OrderDTO>(order));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Fulfilled,Address,City,PostalCode")]
            OrderDTO orderDto)
        {
            if (id != orderDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // NOTE: https://stackoverflow.com/questions/13314666/using-automapper-to-update-an-existing-entity-poco/25242322
                    var order = await _context.Order.FindAsync(id);
                    _mapper.Map(orderDto, order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(orderDto.Id))
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


            return View(orderDto);
        }

        // GET: Order/Delete/4
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<OrderDTO>(order));
        }

        // POST: Order/Delete/3
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Order.FindAsync(id);
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.Id == id);
        }
    }
}