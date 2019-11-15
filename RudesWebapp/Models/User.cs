using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace RudesWebapp.Models
{
    public partial class User : IdentityUser
    {
        public User()
        {
            Order = new HashSet<Order>();
            ShoppingCart = new HashSet<ShoppingCart>();
            RegistrationDate = DateTime.Now;
        }


        // public string Username { get; set; }              // inherited

        [PersonalData] public string Name { get; set; } // TODO or is it [ProtectedPersonalData]

        [PersonalData] public string LastName { get; set; }

        // public string Email { get; set; }                 // inherited
        // public string PasswordHash { get; set; }          // inherited
        // public int? Role { get; set; }                    // TODO - authorisation and roles
        // public string PhoneNumber { get; set; }           // inherited

        public DateTime RegistrationDate { get; set; }

        public virtual ICollection<Order> Order { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCart { get; set; }
    }
}