using System;
using System.Collections.Generic;

namespace RudesWebapp.Models
{
    public partial class Recenzija
    {
        public int Id { get; set; }
        public int IdArtikla { get; set; }
        public DateTime? Datum { get; set; }
        public int? Ocjena { get; set; }
        public string Komentar { get; set; }
        public string KorisnickoIme { get; set; }
        public bool? Blokirano { get; set; }

        public virtual Artikl IdArtiklaNavigation { get; set; }
    }
}
