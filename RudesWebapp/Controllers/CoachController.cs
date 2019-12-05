using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RudesWebapp.Data;
using RudesWebapp.Models;

//[Authorize(Roles = "Coach")]
namespace RudesWebapp.Controllers
{
    public class CoachController : Controller
    {
        private User coach;
        private RudesDatabaseContext _context;

        public CoachController(RudesDatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        /*
        public IActionResult AddPlayer()
        {
            return View();
        }

        public IActionResult ChangePlayerData(int ID_player)
        {
            return View();
        }

        public IActionResult DeletePlayer(int ID_player)
        {
            return View();
        }
        */

        // Player

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayers()
        {
            return await _context.Player.ToListAsync();
        }

        [HttpGet]
        public async Task<ActionResult<Player>> GetPlayer(int id)
        {
            var player = await _context.Player.FindAsync(id);

            if (player == null)
            {
                return NotFound();
            }

            return player;
        }

        [HttpPost]
        public async Task<ActionResult<Player>> AddPlayer(Player player)
        {
            _context.Player.Add(player);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlayer", new {id = player.Id}, player);
        }

        [HttpPut]
        public async Task<ActionResult<Player>> UpdatePlayer(int id, Player player)
        {
            if (id != player.Id)
            {
                return BadRequest();
            }

            _context.Entry(player).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var player_from_database = await _context.Player.FindAsync(id);
                if (player_from_database == null)
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
        public async Task<ActionResult<Player>> RemovePlayer(int id)
        {
            var player = await _context.Player.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }

            _context.Player.Remove(player);
            await _context.SaveChangesAsync();

            return player;
        }
    }
}