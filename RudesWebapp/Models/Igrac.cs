using System;
using System.Collections.Generic;

namespace RudesWebapp.Models
{
    public partial class Igrac
    {
        public int Id { get; set; }
        public DateTime? DatumDodavanja { get; set; }
        public DateTime? DatumPosljednjeIzmjene { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime? DatumRodenja { get; set; }
        public string Pozicija { get; set; }
    }
}
