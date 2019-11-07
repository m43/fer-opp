using System;
using System.Collections.Generic;

namespace RudesWebapp.Models
{
    public partial class Artikl
    {
        public Artikl()
        {
            ArtiklKosarice = new HashSet<ArtiklKosarice>();
            ArtiklNarudzbe = new HashSet<ArtiklNarudzbe>();
            Popust = new HashSet<Popust>();
            Recenzija = new HashSet<Recenzija>();
        }

        public int Id { get; set; }
        public DateTime? DatumDodavanja { get; set; }
        public DateTime? DatumPosljednjeIzmjene { get; set; }
        public string Tip { get; set; }
        public int? Cijena { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public int? IdSlika { get; set; }

        public virtual Slika IdSlikaNavigation { get; set; }
        public virtual ArtiklDostupnost ArtiklDostupnost { get; set; }
        public virtual ICollection<ArtiklKosarice> ArtiklKosarice { get; set; }
        public virtual ICollection<ArtiklNarudzbe> ArtiklNarudzbe { get; set; }
        public virtual ICollection<Popust> Popust { get; set; }
        public virtual ICollection<Recenzija> Recenzija { get; set; }
    }
}
