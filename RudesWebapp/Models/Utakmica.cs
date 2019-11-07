using System;
using System.Collections.Generic;

namespace RudesWebapp.Models
{
    public partial class Utakmica
    {
        public int Id { get; set; }
        public DateTime? Datum { get; set; }
        public string TimDomacin { get; set; }
        public string TimGost { get; set; }
        public string Mjesto { get; set; }
        public string Dvorana { get; set; }
        public string Drzava { get; set; }
    }
}
