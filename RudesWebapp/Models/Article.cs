using System;
using System.Collections.Generic;

namespace RudesWebapp.Models
{
    public partial class Article
    {
        public Article()
        {
            Discount = new HashSet<Discount>();
            OrderArticle = new HashSet<OrderArticle>();
            Review = new HashSet<Review>();
            ShoppingCartArticle = new HashSet<ShoppingCartArticle>();
        }

        public int Id { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? LastModificationDate { get; set; }
        public string Type { get; set; }
        public int? Price { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ImageId { get; set; }

        public virtual Image Image { get; set; }
        public virtual ArticleAvailability ArticleAvailability { get; set; }
        public virtual ICollection<Discount> Discount { get; set; }
        public virtual ICollection<OrderArticle> OrderArticle { get; set; }
        public virtual ICollection<Review> Review { get; set; }
        public virtual ICollection<ShoppingCartArticle> ShoppingCartArticle { get; set; }
    }
}
