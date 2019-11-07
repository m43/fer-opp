using System;
using System.Collections.Generic;

namespace RudesWebapp.Models
{
    public partial class User
    {
        public User()
        {
            Order = new HashSet<Order>();
            ShoppingCart = new HashSet<ShoppingCart>();
        }

        public string Username { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int? Role { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? RegistrationDate { get; set; }

        public virtual ICollection<Order> Order { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCart { get; set; }
    }
}
