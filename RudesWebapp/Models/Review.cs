using System;
using System.ComponentModel.DataAnnotations;

namespace RudesWebapp.Models
{
    public class Review : IDateCreated
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public DateTime CreationDate { get; set; }
        public int? Rating { get; set; }
        public string Comment { get; set; }
        public string UserId { get; set; }
        public bool Blocked { get; set; }

        public virtual User User { get; set; }
        public virtual Article Article { get; set; }
    }
}