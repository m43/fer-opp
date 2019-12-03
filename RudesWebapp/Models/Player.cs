using System;
using System.Collections.Generic;

namespace RudesWebapp.Models
{
    public partial class Player
    {
        public int Id { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? LastModificationDate { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Position { get; set; }
        public int? ImageId { get; set; }
        public virtual Image Image { get; set; }

    }
}
