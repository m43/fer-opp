using System;
using System.Collections.Generic;

namespace RudesWebapp.Models
{
    public partial class Transaction
    {
        public Transaction()
        {
            Order = new HashSet<Order>();
        }

        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public decimal? Amount { get; set; }
        public string Card { get; set; }

        public virtual ICollection<Order> Order { get; set; }
    }
}
