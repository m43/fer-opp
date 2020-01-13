﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RudesWebapp.Data;

namespace RudesWebapp.Migrations
{
    [DbContext(typeof(RudesDatabaseContext))]
    [Migration("20200113143410_FixedShoppingCartAndUser")]
    partial class FixedShoppingCartAndUser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("RudesWebapp.Models.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Argb")
                        .HasColumnName("color")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnName("creation_date")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ImageId")
                        .HasColumnName("image_ID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModificationDate")
                        .HasColumnName("last_modification_date")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("nvarchar(120)")
                        .HasMaxLength(120);

                    b.Property<decimal>("Price")
                        .HasColumnName("price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnName("type")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasAlternateKey("Name");

                    b.HasIndex("ImageId");

                    b.ToTable("article");
                });

            modelBuilder.Entity("RudesWebapp.Models.ArticleAvailability", b =>
                {
                    b.Property<int>("ArticleId")
                        .HasColumnName("article_ID")
                        .HasColumnType("int");

                    b.Property<string>("Size")
                        .HasColumnName("size")
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<int?>("Quantity")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("quantity")
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.HasKey("ArticleId", "Size")
                        .HasName("PK__ArticleAvailability___CC37F2680A55149B");

                    b.ToTable("article_availability");
                });

            modelBuilder.Entity("RudesWebapp.Models.Discount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ArticleId")
                        .HasColumnName("article_ID")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnName("date")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("EndDate")
                        .IsRequired()
                        .HasColumnName("end_date")
                        .HasColumnType("datetime");

                    b.Property<int>("Percentage")
                        .HasColumnName("percentage")
                        .HasColumnType("int");

                    b.Property<DateTime?>("StartDate")
                        .IsRequired()
                        .HasColumnName("start_date")
                        .HasColumnType("datetime");

                    b.HasKey("Id", "ArticleId")
                        .HasName("PK__discount__AED79301C419BEB6");

                    b.HasIndex("ArticleId");

                    b.ToTable("discount");
                });

            modelBuilder.Entity("RudesWebapp.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AltText")
                        .IsRequired()
                        .HasColumnName("alt_text")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Caption")
                        .IsRequired()
                        .HasColumnName("caption")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnName("date")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("OriginalName")
                        .IsRequired()
                        .HasColumnName("original_name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("image");
                });

            modelBuilder.Entity("RudesWebapp.Models.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AwayTeam")
                        .IsRequired()
                        .HasColumnName("away_team")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnName("city")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Country")
                        .HasColumnName("country")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("HomeTeam")
                        .IsRequired()
                        .HasColumnName("home_team")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("SportsHall")
                        .HasColumnName("sports_hall")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<DateTime>("Time")
                        .HasColumnName("date")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.ToTable("match");
                });

            modelBuilder.Entity("RudesWebapp.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnName("address")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("City")
                        .HasColumnName("city")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnName("date")
                        .HasColumnType("datetime");

                    b.Property<bool>("Fulfilled")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("fulfilled")
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int?>("PostalCode")
                        .HasColumnName("postal_code")
                        .HasColumnType("int");

                    b.Property<string>("TransactionId")
                        .HasColumnName("transaction_ID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnName("user_ID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("order");
                });

            modelBuilder.Entity("RudesWebapp.Models.OrderArticle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ArticleId")
                        .HasColumnName("article_ID")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnName("order_ID")
                        .HasColumnType("int");

                    b.Property<int?>("PurchaseDiscount")
                        .IsRequired()
                        .HasColumnName("purchase_discount")
                        .HasColumnType("int");

                    b.Property<decimal>("PurchasePrice")
                        .HasColumnName("purchase_price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int?>("Quantity")
                        .IsRequired()
                        .HasColumnName("quantity")
                        .HasColumnType("int");

                    b.Property<string>("Size")
                        .HasColumnName("size")
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.HasIndex("OrderId");

                    b.ToTable("order_article");
                });

            modelBuilder.Entity("RudesWebapp.Models.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("BirthDate")
                        .IsRequired()
                        .HasColumnName("birth_date")
                        .HasColumnType("date");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnName("creation_date")
                        .HasColumnType("datetime");

                    b.Property<int?>("ImageId")
                        .HasColumnName("image_ID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModificationDate")
                        .HasColumnName("last_modification_date")
                        .HasColumnType("datetime");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnName("last_name")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<int>("PlayerType")
                        .HasColumnName("player_type")
                        .HasColumnType("int");

                    b.Property<int>("Position")
                        .HasColumnName("position")
                        .HasColumnType("int")
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.ToTable("player");
                });

            modelBuilder.Entity("RudesWebapp.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnName("content")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnName("creation_date")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnName("end_date")
                        .HasColumnType("datetime");

                    b.Property<int?>("ImageId")
                        .HasColumnName("image_ID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModificationDate")
                        .HasColumnName("last_modification_date")
                        .HasColumnType("datetime");

                    b.Property<string>("PostType")
                        .HasColumnName("post_type")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("StartDate")
                        .HasColumnName("start_date")
                        .HasColumnType("datetime");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnName("title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.ToTable("post");
                });

            modelBuilder.Entity("RudesWebapp.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ArticleId")
                        .HasColumnName("article_ID")
                        .HasColumnType("int");

                    b.Property<bool>("Blocked")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("blocked")
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnName("comment")
                        .HasColumnType("nvarchar(max)")
                        .HasMaxLength(5000);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnName("creation_date")
                        .HasColumnType("datetime");

                    b.Property<int>("Rating")
                        .HasColumnName("rating")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnName("user_ID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.HasIndex("UserId");

                    b.ToTable("review");
                });

            modelBuilder.Entity("RudesWebapp.Models.ShoppingCart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnName("date_created")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("LastModificationDate")
                        .HasColumnName("last_modification_date")
                        .HasColumnType("datetime");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnName("user_ID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("shopping_cart");
                });

            modelBuilder.Entity("RudesWebapp.Models.ShoppingCartArticle", b =>
                {
                    b.Property<int>("ShoppingCartId")
                        .HasColumnName("shopping_cart_ID")
                        .HasColumnType("int");

                    b.Property<int>("ArticleId")
                        .HasColumnName("article_ID")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnName("quantity")
                        .HasColumnType("int");

                    b.Property<string>("Size")
                        .HasColumnName("size")
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.HasKey("ShoppingCartId", "ArticleId")
                        .HasName("PK__shopping__B7E583C151B3360B");

                    b.HasIndex("ArticleId");

                    b.ToTable("shopping_cart_article");
                });

            modelBuilder.Entity("RudesWebapp.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("RudesWebapp.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("RudesWebapp.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RudesWebapp.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("RudesWebapp.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RudesWebapp.Models.Article", b =>
                {
                    b.HasOne("RudesWebapp.Models.Image", "Image")
                        .WithMany("Article")
                        .HasForeignKey("ImageId")
                        .HasConstraintName("FK__article__image_I__46E78A0C")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("RudesWebapp.Models.ArticleAvailability", b =>
                {
                    b.HasOne("RudesWebapp.Models.Article", "Article")
                        .WithMany("ArticleAvailability")
                        .HasForeignKey("ArticleId")
                        .HasConstraintName("FK__article_a__artic__403A8C7D")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RudesWebapp.Models.Discount", b =>
                {
                    b.HasOne("RudesWebapp.Models.Article", "Article")
                        .WithMany("Discount")
                        .HasForeignKey("ArticleId")
                        .HasConstraintName("FK__discount__articl__44FF419A")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RudesWebapp.Models.Order", b =>
                {
                    b.HasOne("RudesWebapp.Models.User", "User")
                        .WithMany("Order")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__order__user__3D5E1FD2")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RudesWebapp.Models.OrderArticle", b =>
                {
                    b.HasOne("RudesWebapp.Models.Article", "Article")
                        .WithMany("OrderArticle")
                        .HasForeignKey("ArticleId")
                        .HasConstraintName("FK__order_a__article__3F466844")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("RudesWebapp.Models.Order", "Order")
                        .WithMany("OrderArticle")
                        .HasForeignKey("OrderId")
                        .HasConstraintName("FK__order_art__order__412EB0B6")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RudesWebapp.Models.Player", b =>
                {
                    b.HasOne("RudesWebapp.Models.Image", "Image")
                        .WithMany("Player")
                        .HasForeignKey("ImageId")
                        .HasConstraintName("FK__player__image_ID__03122019M43")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("RudesWebapp.Models.Post", b =>
                {
                    b.HasOne("RudesWebapp.Models.Image", "Image")
                        .WithMany("Post")
                        .HasForeignKey("ImageId")
                        .HasConstraintName("FK__post__image_ID__47DBAE45")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("RudesWebapp.Models.Review", b =>
                {
                    b.HasOne("RudesWebapp.Models.Article", "Article")
                        .WithMany("Review")
                        .HasForeignKey("ArticleId")
                        .HasConstraintName("FK__review__article___45F365D3")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RudesWebapp.Models.User", "User")
                        .WithMany("Review")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__review___usern__03122019M43")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("RudesWebapp.Models.ShoppingCart", b =>
                {
                    b.HasOne("RudesWebapp.Models.User", "User")
                        .WithOne("ShoppingCart")
                        .HasForeignKey("RudesWebapp.Models.ShoppingCart", "UserId")
                        .HasConstraintName("FK__shopping___usern__440B1D61")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RudesWebapp.Models.ShoppingCartArticle", b =>
                {
                    b.HasOne("RudesWebapp.Models.Article", "Article")
                        .WithMany("ShoppingCartArticle")
                        .HasForeignKey("ArticleId")
                        .HasConstraintName("FK__shopping___artic__4222D4EF")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RudesWebapp.Models.ShoppingCart", "ShoppingCart")
                        .WithMany("ShoppingCartArticle")
                        .HasForeignKey("ShoppingCartId")
                        .HasConstraintName("FK__shopping___shopp__4316F928")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
