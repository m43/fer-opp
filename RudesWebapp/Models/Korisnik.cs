using System;
using System.Collections.Generic;

namespace RudesWebapp.Models
{
    public partial class Korisnik
    {
        public Korisnik()
        {
            Kosarica = new HashSet<Kosarica>();
            Narudzba = new HashSet<Narudzba>();
        }

        public string KorisnickoIme { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string LozinkaHash { get; set; }
        public int? RazinaOvlasti { get; set; }
        public string BrojMob { get; set; }
        public DateTime? DatumRegistracije { get; set; }

        public virtual ICollection<Kosarica> Kosarica { get; set; }
        public virtual ICollection<Narudzba> Narudzba { get; set; }
    }
}
