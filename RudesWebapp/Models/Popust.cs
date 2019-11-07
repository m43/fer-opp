using System;
using System.Collections.Generic;

namespace RudesWebapp.Models
{
    public partial class Popust
    {
        public int Id { get; set; }
        public int IdArtikla { get; set; }
        public DateTime? Datum { get; set; }
        public DateTime? DatumPocetka { get; set; }
        public DateTime? DatumKraja { get; set; }
        public int? Postotak { get; set; }

        public virtual Artikl IdArtiklaNavigation { get; set; }
    }
}
