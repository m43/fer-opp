using System;
using System.Collections.Generic;

namespace RudesWebapp.Models
{
    public partial class ArtiklDostupnost
    {
        public int IdArtikla { get; set; }
        public string Velicina { get; set; }
        public int? Kolicina { get; set; }

        public virtual Artikl IdArtiklaNavigation { get; set; }
    }
}
