using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RudesWebapp.Dtos;
using RudesWebapp.Models;
using RudesWebapp.Services;

namespace RudesWebapp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.UserOrAbove)]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var result = await _userService.MapCurrentUserToUserDto(User);
            if (!result.Succeeded)
            {
                return NotFound(result);
            }

            return result.Value;
        }
    }
}