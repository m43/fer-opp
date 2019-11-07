using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RudesWebapp.Models
{
    public partial class RudesBazaContext : DbContext
    {
        public RudesBazaContext()
        {
        }

        public RudesBazaContext(DbContextOptions<RudesBazaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Artikl> Artikl { get; set; }
        public virtual DbSet<ArtiklDostupnost> ArtiklDostupnost { get; set; }
        public virtual DbSet<ArtiklKosarice> ArtiklKosarice { get; set; }
        public virtual DbSet<ArtiklNarudzbe> ArtiklNarudzbe { get; set; }
        public virtual DbSet<Igrac> Igrac { get; set; }
        public virtual DbSet<Korisnik> Korisnik { get; set; }
        public virtual DbSet<Kosarica> Kosarica { get; set; }
        public virtual DbSet<Narudzba> Narudzba { get; set; }
        public virtual DbSet<Objava> Objava { get; set; }
        public virtual DbSet<Popust> Popust { get; set; }
        public virtual DbSet<Recenzija> Recenzija { get; set; }
        public virtual DbSet<Slika> Slika { get; set; }
        public virtual DbSet<Transakcija> Transakcija { get; set; }
        public virtual DbSet<Utakmica> Utakmica { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(LocalDB)\\rudes;Database=RudesBaza;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artikl>(entity =>
            {
                entity.ToTable("artikl");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cijena).HasColumnName("cijena");

                entity.Property(e => e.DatumDodavanja)
                    .HasColumnName("datum_dodavanja")
                    .HasColumnType("datetime");

                entity.Property(e => e.DatumPosljednjeIzmjene)
                    .HasColumnName("datum_posljednje_izmjene")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdSlika).HasColumnName("ID_slika");

                entity.Property(e => e.Naziv)
                    .HasColumnName("naziv")
                    .HasMaxLength(255);

                entity.Property(e => e.Opis)
                    .HasColumnName("opis")
                    .HasMaxLength(255);

                entity.Property(e => e.Tip)
                    .HasColumnName("tip")
                    .HasMaxLength(255);

                entity.HasOne(d => d.IdSlikaNavigation)
                    .WithMany(p => p.Artikl)
                    .HasForeignKey(d => d.IdSlika)
                    .HasConstraintName("FK__artikl__ID_slika__46E78A0C");
            });

            modelBuilder.Entity<ArtiklDostupnost>(entity =>
            {
                entity.HasKey(e => e.IdArtikla)
                    .HasName("PK__artikl_d__321CEECE288AB96A");

                entity.ToTable("artikl_dostupnost");

                entity.Property(e => e.IdArtikla)
                    .HasColumnName("ID_artikla")
                    .ValueGeneratedNever();

                entity.Property(e => e.Kolicina).HasColumnName("kolicina");

                entity.Property(e => e.Velicina)
                    .HasColumnName("velicina")
                    .HasMaxLength(255);

                entity.HasOne(d => d.IdArtiklaNavigation)
                    .WithOne(p => p.ArtiklDostupnost)
                    .HasForeignKey<ArtiklDostupnost>(d => d.IdArtikla)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__artikl_do__ID_ar__403A8C7D");
            });

            modelBuilder.Entity<ArtiklKosarice>(entity =>
            {
                entity.HasKey(e => new { e.IdKosarice, e.IdArtikla })
                    .HasName("PK__artikl_k__9D8568CCDB49831C");

                entity.ToTable("artikl_kosarice");

                entity.Property(e => e.IdKosarice).HasColumnName("ID_kosarice");

                entity.Property(e => e.IdArtikla).HasColumnName("ID_artikla");

                entity.Property(e => e.Kolicina).HasColumnName("kolicina");

                entity.Property(e => e.KupovnaCijena)
                    .HasColumnName("kupovna_cijena")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.KupovniPopust).HasColumnName("kupovni_popust");

                entity.Property(e => e.Velicina)
                    .HasColumnName("velicina")
                    .HasMaxLength(255);

                entity.HasOne(d => d.IdArtiklaNavigation)
                    .WithMany(p => p.ArtiklKosarice)
                    .HasForeignKey(d => d.IdArtikla)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__artikl_ko__ID_ar__4222D4EF");

                entity.HasOne(d => d.IdKosariceNavigation)
                    .WithMany(p => p.ArtiklKosarice)
                    .HasForeignKey(d => d.IdKosarice)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__artikl_ko__ID_ko__4316F928");
            });

            modelBuilder.Entity<ArtiklNarudzbe>(entity =>
            {
                entity.HasKey(e => new { e.IdNarudzbe, e.IdArtikla })
                    .HasName("PK__artikl_n__97E8649ED24A0BDB");

                entity.ToTable("artikl_narudzbe");

                entity.Property(e => e.IdNarudzbe).HasColumnName("ID_narudzbe");

                entity.Property(e => e.IdArtikla).HasColumnName("ID_artikla");

                entity.Property(e => e.Kolicina).HasColumnName("kolicina");

                entity.Property(e => e.KupovnaCijena)
                    .HasColumnName("kupovna_cijena")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.KupovniPopust).HasColumnName("kupovni_popust");

                entity.Property(e => e.Velicina)
                    .HasColumnName("velicina")
                    .HasMaxLength(255);

                entity.HasOne(d => d.IdArtiklaNavigation)
                    .WithMany(p => p.ArtiklNarudzbe)
                    .HasForeignKey(d => d.IdArtikla)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__artikl_na__ID_ar__3F466844");

                entity.HasOne(d => d.IdNarudzbeNavigation)
                    .WithMany(p => p.ArtiklNarudzbe)
                    .HasForeignKey(d => d.IdNarudzbe)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__artikl_na__ID_na__412EB0B6");
            });

            modelBuilder.Entity<Igrac>(entity =>
            {
                entity.ToTable("igrac");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DatumDodavanja)
                    .HasColumnName("datum_dodavanja")
                    .HasColumnType("datetime");

                entity.Property(e => e.DatumPosljednjeIzmjene)
                    .HasColumnName("datum_posljednje_izmjene")
                    .HasColumnType("datetime");

                entity.Property(e => e.DatumRodenja)
                    .HasColumnName("datum_rodenja")
                    .HasColumnType("date");

                entity.Property(e => e.Ime)
                    .HasColumnName("ime")
                    .HasMaxLength(255);

                entity.Property(e => e.Pozicija)
                    .HasColumnName("pozicija")
                    .HasMaxLength(255);

                entity.Property(e => e.Prezime)
                    .HasColumnName("prezime")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Korisnik>(entity =>
            {
                entity.HasKey(e => e.KorisnickoIme)
                    .HasName("PK__korisnik__ACB74FFDE3D37DEE");

                entity.ToTable("korisnik");

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__korisnik__AB6E61643896C425")
                    .IsUnique();

                entity.Property(e => e.KorisnickoIme)
                    .HasColumnName("korisnicko_ime")
                    .HasMaxLength(255);

                entity.Property(e => e.BrojMob)
                    .HasColumnName("broj_mob")
                    .HasMaxLength(255);

                entity.Property(e => e.DatumRegistracije)
                    .HasColumnName("datum_registracije")
                    .HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(255);

                entity.Property(e => e.Ime)
                    .HasColumnName("ime")
                    .HasMaxLength(255);

                entity.Property(e => e.LozinkaHash)
                    .HasColumnName("lozinka_hash")
                    .HasMaxLength(255);

                entity.Property(e => e.Prezime)
                    .HasColumnName("prezime")
                    .HasMaxLength(255);

                entity.Property(e => e.RazinaOvlasti).HasColumnName("razina_ovlasti");
            });

            modelBuilder.Entity<Kosarica>(entity =>
            {
                entity.ToTable("kosarica");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Datum)
                    .HasColumnName("datum")
                    .HasColumnType("datetime");

                entity.Property(e => e.KorisnickoIme)
                    .HasColumnName("korisnicko_ime")
                    .HasMaxLength(255);

                entity.HasOne(d => d.KorisnickoImeNavigation)
                    .WithMany(p => p.Kosarica)
                    .HasForeignKey(d => d.KorisnickoIme)
                    .HasConstraintName("FK__kosarica__korisn__440B1D61");
            });

            modelBuilder.Entity<Narudzba>(entity =>
            {
                entity.ToTable("narudzba");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Adresa)
                    .HasColumnName("adresa")
                    .HasMaxLength(255);

                entity.Property(e => e.Datum)
                    .HasColumnName("datum")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdTransakcije).HasColumnName("ID_transakcije");

                entity.Property(e => e.KorisnickoIme)
                    .HasColumnName("korisnicko_ime")
                    .HasMaxLength(255);

                entity.Property(e => e.Mjesto)
                    .HasColumnName("mjesto")
                    .HasMaxLength(255);

                entity.Property(e => e.PostanskiBroj).HasColumnName("postanski_broj");

                entity.Property(e => e.Zaprimljenost).HasColumnName("zaprimljenost");

                entity.HasOne(d => d.IdTransakcijeNavigation)
                    .WithMany(p => p.Narudzba)
                    .HasForeignKey(d => d.IdTransakcije)
                    .HasConstraintName("FK__narudzba__ID_tra__3E52440B");

                entity.HasOne(d => d.KorisnickoImeNavigation)
                    .WithMany(p => p.Narudzba)
                    .HasForeignKey(d => d.KorisnickoIme)
                    .HasConstraintName("FK__narudzba__korisn__3D5E1FD2");
            });

            modelBuilder.Entity<Objava>(entity =>
            {
                entity.ToTable("objava");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DatumIsteka)
                    .HasColumnName("datum_isteka")
                    .HasColumnType("datetime");

                entity.Property(e => e.DatumObjave)
                    .HasColumnName("datum_objave")
                    .HasColumnType("datetime");

                entity.Property(e => e.DatumPocetka)
                    .HasColumnName("datum_pocetka")
                    .HasColumnType("datetime");

                entity.Property(e => e.DatumPosljednjeIzmjene)
                    .HasColumnName("datum_posljednje_izmjene")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdSlika).HasColumnName("ID_slika");

                entity.Property(e => e.Sadrzaj)
                    .HasColumnName("sadrzaj")
                    .HasMaxLength(255);

                entity.Property(e => e.VrstaObjave)
                    .HasColumnName("vrsta_objave")
                    .HasMaxLength(255);

                entity.HasOne(d => d.IdSlikaNavigation)
                    .WithMany(p => p.Objava)
                    .HasForeignKey(d => d.IdSlika)
                    .HasConstraintName("FK__objava__ID_slika__47DBAE45");
            });

            modelBuilder.Entity<Popust>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.IdArtikla })
                    .HasName("PK__popust__C13522CB2AB233D2");

                entity.ToTable("popust");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IdArtikla).HasColumnName("ID_artikla");

                entity.Property(e => e.Datum)
                    .HasColumnName("datum")
                    .HasColumnType("datetime");

                entity.Property(e => e.DatumKraja)
                    .HasColumnName("datum_kraja")
                    .HasColumnType("datetime");

                entity.Property(e => e.DatumPocetka)
                    .HasColumnName("datum_pocetka")
                    .HasColumnType("datetime");

                entity.Property(e => e.Postotak).HasColumnName("postotak");

                entity.HasOne(d => d.IdArtiklaNavigation)
                    .WithMany(p => p.Popust)
                    .HasForeignKey(d => d.IdArtikla)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__popust__ID_artik__44FF419A");
            });

            modelBuilder.Entity<Recenzija>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.IdArtikla })
                    .HasName("PK__recenzij__C13522CB618A7CCA");

                entity.ToTable("recenzija");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IdArtikla).HasColumnName("ID_artikla");

                entity.Property(e => e.Blokirano).HasColumnName("blokirano");

                entity.Property(e => e.Datum)
                    .HasColumnName("datum")
                    .HasColumnType("datetime");

                entity.Property(e => e.Komentar)
                    .HasColumnName("komentar")
                    .HasMaxLength(255);

                entity.Property(e => e.KorisnickoIme)
                    .HasColumnName("korisnicko_ime")
                    .HasMaxLength(255);

                entity.Property(e => e.Ocjena).HasColumnName("ocjena");

                entity.HasOne(d => d.IdArtiklaNavigation)
                    .WithMany(p => p.Recenzija)
                    .HasForeignKey(d => d.IdArtikla)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__recenzija__ID_ar__45F365D3");
            });

            modelBuilder.Entity<Slika>(entity =>
            {
                entity.ToTable("slika");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Datum)
                    .HasColumnName("datum")
                    .HasColumnType("datetime");

                entity.Property(e => e.Path)
                    .HasColumnName("path")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Transakcija>(entity =>
            {
                entity.ToTable("transakcija");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Datum)
                    .HasColumnName("datum")
                    .HasColumnType("datetime");

                entity.Property(e => e.Iznos)
                    .HasColumnName("iznos")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Kartica)
                    .HasColumnName("kartica")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Utakmica>(entity =>
            {
                entity.ToTable("utakmica");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Datum)
                    .HasColumnName("datum")
                    .HasColumnType("datetime");

                entity.Property(e => e.Drzava)
                    .HasColumnName("drzava")
                    .HasMaxLength(255);

                entity.Property(e => e.Dvorana)
                    .HasColumnName("dvorana")
                    .HasMaxLength(255);

                entity.Property(e => e.Mjesto)
                    .HasColumnName("mjesto")
                    .HasMaxLength(255);

                entity.Property(e => e.TimDomacin)
                    .HasColumnName("tim_domacin")
                    .HasMaxLength(255);

                entity.Property(e => e.TimGost)
                    .HasColumnName("tim_gost")
                    .HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
