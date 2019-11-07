using System;
using System.Collections.Generic;

namespace RudesWebapp.Models
{
    public partial class ArtiklNarudzbe
    {
        public int IdNarudzbe { get; set; }
        public int IdArtikla { get; set; }
        public int? Kolicina { get; set; }
        public string Velicina { get; set; }
        public decimal? KupovnaCijena { get; set; }
        public int? KupovniPopust { get; set; }

        public virtual Artikl IdArtiklaNavigation { get; set; }
        public virtual Narudzba IdNarudzbeNavigation { get; set; }
    }
}
