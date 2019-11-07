using System;
using System.Collections.Generic;

namespace RudesWebapp.Models
{
    public partial class Narudzba
    {
        public Narudzba()
        {
            ArtiklNarudzbe = new HashSet<ArtiklNarudzbe>();
        }

        public int Id { get; set; }
        public int? IdTransakcije { get; set; }
        public string KorisnickoIme { get; set; }
        public DateTime? Datum { get; set; }
        public bool? Zaprimljenost { get; set; }
        public string Adresa { get; set; }
        public string Mjesto { get; set; }
        public int? PostanskiBroj { get; set; }

        public virtual Transakcija IdTransakcijeNavigation { get; set; }
        public virtual Korisnik KorisnickoImeNavigation { get; set; }
        public virtual ICollection<ArtiklNarudzbe> ArtiklNarudzbe { get; set; }
    }
}
