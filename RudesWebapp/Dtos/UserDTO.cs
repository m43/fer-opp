using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RudesWebapp.Dtos
{
    public class UserDTO
    {
        [PersonalData]
        public string Name { get; set; } // TODO or is it [ProtectedPersonalData]
        [PersonalData] 
        public string LastName { get; set; }
    }
}
