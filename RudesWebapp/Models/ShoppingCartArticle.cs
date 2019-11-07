using System;
using System.Collections.Generic;

namespace RudesWebapp.Models
{
    public partial class ShoppingCartArticle
    {
        public int ShoppingCartId { get; set; }
        public int ArticleId { get; set; }
        public int? Quantity { get; set; }
        public string Size { get; set; }
        public decimal? PurchasePrice { get; set; }
        public int? PurchaseDiscount { get; set; }

        public virtual Article Article { get; set; }
        public virtual ShoppingCart ShoppingCart { get; set; }
    }
}
