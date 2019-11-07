using System;
using System.Collections.Generic;

namespace RudesWebapp.Models
{
    public partial class Objava
    {
        public int Id { get; set; }
        public string Sadrzaj { get; set; }
        public int? IdSlika { get; set; }
        public DateTime? DatumObjave { get; set; }
        public DateTime? DatumPosljednjeIzmjene { get; set; }
        public string VrstaObjave { get; set; }
        public DateTime? DatumPocetka { get; set; }
        public DateTime? DatumIsteka { get; set; }

        public virtual Slika IdSlikaNavigation { get; set; }
    }
}
