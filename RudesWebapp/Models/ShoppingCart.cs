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
        public string UserId { get; set; }
        public DateTime? Date { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<ShoppingCartArticle> ShoppingCartArticle { get; set; }
    }
}