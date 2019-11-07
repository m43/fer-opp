using System;
using System.Collections.Generic;

namespace RudesWebapp.Models
{
    public partial class Transakcija
    {
        public Transakcija()
        {
            Narudzba = new HashSet<Narudzba>();
        }

        public int Id { get; set; }
        public DateTime? Datum { get; set; }
        public decimal? Iznos { get; set; }
        public string Kartica { get; set; }

        public virtual ICollection<Narudzba> Narudzba { get; set; }
    }
}
