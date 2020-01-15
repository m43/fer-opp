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
        
        public static async Task<bool> SetRole(this User user, UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager, string role)
        {
            if (!await Roles.CheckRoleExists(roleManager, role))
                return false;
            
            var roles = await userManager.GetRolesAsync(user);
            foreach (var currentRole in roles)
            {
                await userManager.RemoveFromRoleAsync(user, currentRole);
            }

            await userManager.AddToRoleAsync(user, role);
            return true;
        }
    }
}