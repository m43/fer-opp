using System;
using System.Collections.Generic;

namespace RudesWebapp.Models
{
    public partial class Kosarica
    {
        public Kosarica()
        {
            ArtiklKosarice = new HashSet<ArtiklKosarice>();
        }

        public int Id { get; set; }
        public string KorisnickoIme { get; set; }
        public DateTime? Datum { get; set; }

        public virtual Korisnik KorisnickoImeNavigation { get; set; }
        public virtual ICollection<ArtiklKosarice> ArtiklKosarice { get; set; }
    }
}
