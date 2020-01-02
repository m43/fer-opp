using System;

namespace RudesWebapp.Models
{
    public partial class Post : IDateCreatedAndUpdated
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int? ImageId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? LastModificationDate { get; set; }
        public string PostType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual Image Image { get; set; }
    }
}