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
        public int? IdTransaction { get; set; }
        public string Username { get; set; }
        public DateTime? Date { get; set; }
        public bool? Fulfilled { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }
        public int? PostalCode { get; set; }

        public virtual Transaction IdTransactionNavigation { get; set; }
        public virtual User UsernameNavigation { get; set; }
        public virtual ICollection<OrderArticle> OrderArticle { get; set; }
    }
}
