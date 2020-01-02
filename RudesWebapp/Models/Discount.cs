using System;
using System.Collections.Generic;

namespace RudesWebapp.Models
{
    public partial class Discount : IDateCreated
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Percentage { get; set; }

        public virtual Article Article { get; set; }
    }
}
