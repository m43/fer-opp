using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using RudesWebapp.Dtos;
using RudesWebapp.Helpers;
using RudesWebapp.Models;

namespace RudesWebapp.Services
{
    public class UserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ServiceResult<UserDTO>> MapCurrentUserToUserDto(ClaimsPrincipal claims)
        {
            var user = await _userManager.GetUserAsync(claims);
            if (user == null)
            {
                return new ServiceResult<UserDTO>(new ServiceError
                    {Property = string.Empty, Description = "No user currently logged in."});
            }

            return ServiceResult<UserDTO>.FromValue(await MapUserToUserDto(user));
        }

        public async Task<UserDTO> MapUserToUserDto(User user)
        {
            return new UserDTO
            {
                Email = user.Email,
                FirstName = user.Name,
                LastName = user.LastName,
                Role = await user.GetRole(_userManager)
            };
        }
    }
}