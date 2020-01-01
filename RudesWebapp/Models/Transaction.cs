using System;

namespace RudesWebapp.Models
{
    public partial class Transaction
    {
        public Transaction()
        {
        }

        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public decimal? Amount { get; set; }
        public string Card { get; set; }

        public int orderId { get; set; }
        public virtual Order Order { get; set; }
    }
}