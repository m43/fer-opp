using System;
using System.Collections.Generic;

namespace RudesWebapp.Models
{
    public partial class Image : IDateCreated
    {
        public Image()
        {
            Article = new HashSet<Article>();
            Post = new HashSet<Post>();
            Player = new HashSet<Player>();
        }

        public int Id { get; set; }

        public string Name { get; set; } // Name aka path to file
        // Name should correspond to the path of the saved static&public image resource
        // For example "/uploads/images/image_of_rudes.png"
        // Therefore it needs to be unique. It is crafted from the OriginalName to have an
        // unique suffix added if necessary (for ex. "/uploads/images/image_of_rudes_2.png"
        // if "/uploads/images/image_of_rudes.png" already exists) and a guessed/checked
        // file extension. For example .exe files should be rejected in the first place,
        // and .jpg .png etc. should be checked anyway. Don't trust the users input!

        public string OriginalName { get; set; }

        // OriginalName should correspond to the name of the uploaded file, ex. "image_of_rudes.png"
        public string Caption { get; set; }
        public string AltText { get; set; }

        public DateTime CreationDate { get; set; }

        public virtual ICollection<Article> Article { get; set; }
        public virtual ICollection<Post> Post { get; set; }
        public virtual ICollection<Player> Player { get; set; }
    }
}