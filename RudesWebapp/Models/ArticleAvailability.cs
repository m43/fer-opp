using System;
using System.Collections.Generic;

namespace RudesWebapp.Models
{
    public partial class ArticleAvailability
    {
        public int ArticleId { get; set; }
        public string Size { get; set; }
        public int? Quantity { get; set; }

        public virtual Article Article { get; set; }
    }
}
