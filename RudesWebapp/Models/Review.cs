using System;
using System.Collections.Generic;

namespace RudesWebapp.Models
{
    public partial class Review
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public DateTime? Date { get; set; }
        public int? Rating { get; set; }
        public string Comment { get; set; }
        public string Username { get; set; }
        public bool? Blocked { get; set; }

        public virtual Article Article { get; set; }
    }
}
