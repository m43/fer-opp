using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RudesWebapp.Models
{
    public class Order : IDateCreatedAndUpdated
    {
        public Order()
        {
            OrderArticle = new HashSet<OrderArticle>();
        }

        public int Id { get; set; }

        public string? UserId { get; set; }

        // Will be automatically assigned in context
        public DateTime CreationDate { get; set; }

        // Will be automatically assigned in context
        [Display(Name = "Last modified")]
        public DateTime? LastModificationDate { get; set; }

        // Should be assigned in service layer. Initially null
        [Display(Name = "Modified by")]
        public string? UserWhoModifiedLastEmail { get; set; }

        public bool Fulfilled { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public int? PostalCode { get; set; }

        public string TransactionId { get; set; } // an identifier that the payment processor gives
        // Order implies that an transaction was made
        // No transaction model is needed, only the above identifier
        // Account information is not saved

        public virtual User User { get; set; }
        public virtual ICollection<OrderArticle> OrderArticle { get; set; }
    }
}