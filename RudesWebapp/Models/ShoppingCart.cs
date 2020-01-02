using System;
using System.Collections.Generic;

namespace RudesWebapp.Models
{
    public partial class ShoppingCart : IDateCreatedAndUpdated
    {
        public ShoppingCart()
        {
            ShoppingCartArticle = new HashSet<ShoppingCartArticle>();
        }

        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? LastModificationDate { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<ShoppingCartArticle> ShoppingCartArticle { get; set; }
    }
}