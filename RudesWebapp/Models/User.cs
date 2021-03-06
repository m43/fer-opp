using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace RudesWebapp.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            Order = new HashSet<Order>();
            RegistrationDate = DateTime.Now;
            Review = new HashSet<Review>();
        }

        [PersonalData] public string Name { get; set; } // TODO or is it [ProtectedPersonalData]
        [PersonalData] public string LastName { get; set; }
        public DateTime RegistrationDate { get; set; }

        public virtual ICollection<Order> Order { get; set; }
        public virtual ShoppingCart ShoppingCart { get; set; }
        public virtual ICollection<Review> Review { get; set; }
    }
}