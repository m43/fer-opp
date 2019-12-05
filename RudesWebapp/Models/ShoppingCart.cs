using System;
using System.Collections.Generic;

namespace RudesWebapp.Models
{
    public partial class ShoppingCart
    {
        public ShoppingCart()
        {
            ShoppingCartArticle = new HashSet<ShoppingCartArticle>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public DateTime? Date { get; set; }

        public virtual User UsernameNavigation { get; set; }
        public virtual ICollection<ShoppingCartArticle> ShoppingCartArticle { get; set; }
    }
}