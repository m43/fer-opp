using System;
using System.Collections.Generic;

namespace RudesWebapp.Models
{
    public partial class Post
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int? ImageId { get; set; }
        public DateTime? PostDate { get; set; }
        public DateTime? LastModificationDate { get; set; }
        public string PostType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual Image Image { get; set; }
    }
}
