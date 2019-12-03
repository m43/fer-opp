using System;
using System.Collections.Generic;

namespace RudesWebapp.Models
{
    public partial class Image
    {
        public Image()
        {
            Article = new HashSet<Article>();
            Post = new HashSet<Post>();
            Player = new HashSet<Player>();
        }

        public int Id { get; set; }
        public string Path { get; set; }
        public DateTime? Date { get; set; }

        public virtual ICollection<Article> Article { get; set; }
        public virtual ICollection<Post> Post { get; set; }
        public virtual ICollection<Player> Player { get; set; }

    }
}
