using System;
using System.Collections.Generic;

namespace RudesWebapp.Models
{
    public partial class Slika
    {
        public Slika()
        {
            Artikl = new HashSet<Artikl>();
            Objava = new HashSet<Objava>();
        }

        public int Id { get; set; }
        public string Path { get; set; }
        public DateTime? Datum { get; set; }

        public virtual ICollection<Artikl> Artikl { get; set; }
        public virtual ICollection<Objava> Objava { get; set; }
    }
}
