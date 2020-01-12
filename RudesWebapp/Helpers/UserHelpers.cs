using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

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
    }
}