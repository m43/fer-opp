using Microsoft.AspNetCore.Identity;

namespace RudesWebapp.Dtos
{
    public class UserDTO
    {
        [PersonalData] public string Name { get; set; } // TODO or is it [ProtectedPersonalData]
        [PersonalData] public string LastName { get; set; }
    }
}