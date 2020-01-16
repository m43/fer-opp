using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace RudesWebapp.Models
{
    public class Article : IDateCreatedAndUpdated
    {
        public Article()
        {
            Discount = new HashSet<Discount>();
            OrderArticle = new HashSet<OrderArticle>();
            Review = new HashSet<Review>();
            ShoppingCartArticle = new HashSet<ShoppingCartArticle>();
        }

        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? LastModificationDate { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ImageId { get; set; }

        // NOTE: Took color for granted. Articles should not have hex color but string color. gg wp.
        //       The color of an article should be either a string or an ColorID that
        //       navigates to a model specifically made to store colors.
        public int? Argb
        {
            get => ArticleColor?.ToArgb();
            set => ArticleColor = value == null ? (Color?) null : Color.FromArgb(value.Value);
        }
        [NotMapped] public Color? ArticleColor { get; set; }

        public virtual Image Image { get; set; }
        public virtual ICollection<ArticleAvailability> ArticleAvailability { get; set; }
        public virtual ICollection<Discount> Discount { get; set; }
        public virtual ICollection<OrderArticle> OrderArticle { get; set; }
        public virtual ICollection<Review> Review { get; set; }
        public virtual ICollection<ShoppingCartArticle> ShoppingCartArticle { get; set; }
    }
}