using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RudesWebapp.Models;

namespace RudesWebapp.Data
{
    public partial class RudesDatabaseContext : IdentityDbContext<User>
    {
        public RudesDatabaseContext()
        {
        }

        public RudesDatabaseContext(DbContextOptions<RudesDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Article> Article { get; set; }
        public virtual DbSet<ArticleAvailability> ArticleAvailability { get; set; }
        public virtual DbSet<Discount> Discount { get; set; }
        public virtual DbSet<Image> Image { get; set; }
        public virtual DbSet<Match> Match { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderArticle> OrderArticle { get; set; }
        public virtual DbSet<Player> Player { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<Review> Review { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCart { get; set; }
        public virtual DbSet<ShoppingCartArticle> ShoppingCartArticle { get; set; }
        public virtual DbSet<Transaction> Transaction { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // TODO not sure if needed

            modelBuilder.Entity<Article>(entity =>
            {
                entity.ToTable("article");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate)
                    .HasColumnName("creation_date")
                    .HasColumnType("datetime")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(255);

                entity.Property(e => e.ImageId).HasColumnName("image_ID");

                entity.Property(e => e.LastModificationDate)
                    .HasColumnName("last_modification_date")
                    .HasColumnType("datetime")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(255);

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(255);

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.Article)
                    .HasForeignKey(d => d.ImageId)
                    .HasConstraintName("FK__article__image_I__46E78A0C");
            });

            modelBuilder.Entity<ArticleAvailability>(entity =>
            {
                entity.HasKey(e => new {e.ArticleId, e.Size})
                    .HasName("PK__ArticleAvailability___CC37F2680A55149B");

                entity.ToTable("article_availability");

                entity.Property(e => e.ArticleId)
                    .HasColumnName("article_ID");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Size)
                    .HasColumnName("size")
                    .HasMaxLength(255);

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.ArticleAvailability)
                    .HasForeignKey(d => d.ArticleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__article_a__artic__403A8C7D");
            });

            modelBuilder.Entity<Discount>(entity =>
            {
                entity.HasKey(e => new {e.Id, e.ArticleId})
                    .HasName("PK__discount__AED79301C419BEB6");

                entity.ToTable("discount");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ArticleId).HasColumnName("article_ID");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.EndDate)
                    .HasColumnName("end_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Percentage).HasColumnName("percentage");

                entity.Property(e => e.StartDate)
                    .HasColumnName("start_date")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.Discount)
                    .HasForeignKey(d => d.ArticleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__discount__articl__44FF419A");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.ToTable("image");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Path)
                    .HasColumnName("path")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Match>(entity =>
            {
                entity.ToTable("match");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AwayTeam)
                    .HasColumnName("away_team")
                    .HasMaxLength(255);

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(255);

                entity.Property(e => e.Country)
                    .HasColumnName("country")
                    .HasMaxLength(255);

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.HomeTeam)
                    .HasColumnName("home_team")
                    .HasMaxLength(255);

                entity.Property(e => e.SportsHall)
                    .HasColumnName("sports_hall")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("order");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(255);

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(255);

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fulfilled).HasColumnName("fulfilled");

                entity.Property(e => e.PostalCode).HasColumnName("postal_code");

                entity.Property(e => e.Username)
                    .HasColumnName("username");
                //.HasMaxLength(255);

                entity.HasOne(d => d.TransactionNavigation)
                    .WithOne(p => p.Order)
                    .HasForeignKey<Transaction>(t => t.orderId)
                    .HasConstraintName("FK__order__ID_transa__3E52440B")
                    .IsRequired(false);

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.Username)
                    .HasConstraintName("FK__order__username__3D5E1FD2");
            });

            modelBuilder.Entity<OrderArticle>(entity =>
            {
                entity.HasKey(e => new {e.OrderId, e.ArticleId})
                    .HasName("PK__order_ar__DA851AC7823BC41D");

                entity.ToTable("order_article");

                entity.Property(e => e.OrderId).HasColumnName("order_ID");

                entity.Property(e => e.ArticleId).HasColumnName("article_ID");

                entity.Property(e => e.PurchaseDiscount).HasColumnName("purchase_discount");

                entity.Property(e => e.PurchasePrice)
                    .HasColumnName("purchase_price")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Size)
                    .HasColumnName("size")
                    .HasMaxLength(255);

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.OrderArticle)
                    .HasForeignKey(d => d.ArticleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__order_art__artic__3F466844");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderArticle)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__order_art__order__412EB0B6");
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.ToTable("player");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BirthDate)
                    .HasColumnName("birth_date")
                    .HasColumnType("date");

                entity.Property(e => e.CreationDate)
                    .HasColumnName("creation_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.LastModificationDate)
                    .HasColumnName("last_modification_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.LastName)
                    .HasColumnName("last_name")
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(255);

                entity.Property(e => e.Position)
                    .HasColumnName("position")
                    .HasMaxLength(255);

                entity.Property(e => e.ImageId).HasColumnName("image_ID");

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.Player)
                    .HasForeignKey(d => d.ImageId)
                    .HasConstraintName("FK__player__image_ID__03122019M43");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("post");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Content)
                    .HasColumnName("content")
                    .HasColumnType("text");

                entity.Property(e => e.Title)
                    .HasColumnName("title");

                entity.Property(e => e.EndDate)
                    .HasColumnName("end_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ImageId).HasColumnName("image_ID");

                entity.Property(e => e.LastModificationDate)
                    .HasColumnName("last_modification_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.PostDate)
                    .HasColumnName("post_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.PostType)
                    .HasColumnName("post_type")
                    .HasMaxLength(255);

                entity.Property(e => e.StartDate)
                    .HasColumnName("start_date")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.Post)
                    .HasForeignKey(d => d.ImageId)
                    .HasConstraintName("FK__post__image_ID__47DBAE45");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(e => new {e.Id, e.ArticleId})
                    .HasName("PK__review__AED793010ECF51E0");

                entity.ToTable("review");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ArticleId).HasColumnName("article_ID");

                entity.Property(e => e.Blocked).HasColumnName("blocked");

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasMaxLength(255);

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.Username)
                    .HasColumnName("username");
                //.HasMaxLength(255);

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.Review)
                    .HasForeignKey(d => d.ArticleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__review__article___45F365D3");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.Review)
                    .HasForeignKey(d => d.Username)
                    .HasConstraintName("FK__review___usern__03122019M43");
            });

            modelBuilder.Entity<ShoppingCart>(entity =>
            {
                entity.ToTable("shopping_cart");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Username)
                    .HasColumnName("username");
                //.HasMaxLength(255);

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.ShoppingCart)
                    .HasForeignKey(d => d.Username)
                    .HasConstraintName("FK__shopping___usern__440B1D61");
            });

            modelBuilder.Entity<ShoppingCartArticle>(entity =>
            {
                entity.HasKey(e => new {e.ShoppingCartId, e.ArticleId})
                    .HasName("PK__shopping__B7E583C151B3360B");

                entity.ToTable("shopping_cart_article");

                entity.Property(e => e.ShoppingCartId).HasColumnName("shopping_cart_ID");

                entity.Property(e => e.ArticleId).HasColumnName("article_ID");

                entity.Property(e => e.PurchaseDiscount).HasColumnName("purchase_discount");

                entity.Property(e => e.PurchasePrice)
                    .HasColumnName("purchase_price")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Size)
                    .HasColumnName("size")
                    .HasMaxLength(255);

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.ShoppingCartArticle)
                    .HasForeignKey(d => d.ArticleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__shopping___artic__4222D4EF");

                entity.HasOne(d => d.ShoppingCart)
                    .WithMany(p => p.ShoppingCartArticle)
                    .HasForeignKey(d => d.ShoppingCartId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__shopping___shopp__4316F928");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("transaction");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Card)
                    .HasColumnName("card")
                    .HasMaxLength(255);

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}