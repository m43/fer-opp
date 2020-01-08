using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RudesWebapp.Data;
using Z.EntityFramework.Plus;

namespace RudesWebapp.Models
{
    public class ShoppingCart : IDateCreatedAndUpdated
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