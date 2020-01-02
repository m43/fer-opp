using System;
using System.Collections.Generic;

namespace RudesWebapp.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderArticle = new HashSet<OrderArticle>();
        }

        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime? Date { get; set; }
        public bool? Fulfilled { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int? PostalCode { get; set; }

        // public virtual Transaction TransactionNavigation { get; set; }
        // Order implies that an transaction was made. No transaction model is needed.
        // Account information is not saved.

        public virtual User User { get; set; }
        public virtual ICollection<OrderArticle> OrderArticle { get; set; }
    }
}