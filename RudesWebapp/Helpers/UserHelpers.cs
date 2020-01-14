using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using RudesWebapp.Models;

namespace RudesWebapp.Helpers
{
    public static class UserHelpers
    {
        public static string GetUserId(this IPrincipal principal)
        {
            // https://entityframeworkcore.com/knowledge-base/38543193/proper-way-to-get-current-user-id-in-entity-framework-core
            var claimsIdentity = (ClaimsIdentity) principal.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            return claim.Value;
        }

        public static bool IsInRoles(this IPrincipal principal, string roles)
        {
            return roles.Split(", ").Any(principal.IsInRole);
        }

        public static async Task SetRole(this IPrincipal principal, UserManager<User> userManager, string role)
        {
            if (role == null)
            {
                throw new ArgumentException("Given role cannot be null.");
            }

            User user = null; // TODO
            var roles = await userManager.GetRolesAsync(user);
            foreach (var currentRoles in roles)
            {
                await userManager.RemoveFromRoleAsync(user, currentRoles);
            }

            await userManager.AddToRoleAsync(user, role);
        }
    }
}