using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RudesWebapp.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    RegistrationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "image",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    path = table.Column<string>(maxLength: 255, nullable: true),
                    date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_image", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "match",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<DateTime>(type: "datetime", nullable: true),
                    home_team = table.Column<string>(maxLength: 255, nullable: true),
                    away_team = table.Column<string>(maxLength: 255, nullable: true),
                    city = table.Column<string>(maxLength: 255, nullable: true),
                    sports_hall = table.Column<string>(maxLength: 255, nullable: true),
                    country = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_match", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "transaction",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<DateTime>(type: "datetime", nullable: true),
                    amount = table.Column<decimal>(type: "decimal(18, 0)", nullable: true),
                    card = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transaction", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "shopping_cart",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(nullable: true),
                    date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shopping_cart", x => x.ID);
                    table.ForeignKey(
                        name: "FK__shopping___usern__440B1D61",
                        column: x => x.username,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "article",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    creation_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    last_modification_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    type = table.Column<string>(maxLength: 255, nullable: true),
                    price = table.Column<int>(nullable: true),
                    name = table.Column<string>(maxLength: 255, nullable: true),
                    description = table.Column<string>(maxLength: 255, nullable: true),
                    image_ID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_article", x => x.ID);
                    table.ForeignKey(
                        name: "FK__article__image_I__46E78A0C",
                        column: x => x.image_ID,
                        principalTable: "image",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "player",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    creation_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    last_modification_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    name = table.Column<string>(maxLength: 255, nullable: true),
                    last_name = table.Column<string>(maxLength: 255, nullable: true),
                    birth_date = table.Column<DateTime>(type: "date", nullable: true),
                    position = table.Column<string>(maxLength: 255, nullable: true),
                    image_ID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_player", x => x.ID);
                    table.ForeignKey(
                        name: "FK__player__image_ID__03122019M43",
                        column: x => x.image_ID,
                        principalTable: "image",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "post",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(nullable: true),
                    content = table.Column<string>(type: "text", nullable: true),
                    image_ID = table.Column<int>(nullable: true),
                    post_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    last_modification_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    post_type = table.Column<string>(maxLength: 255, nullable: true),
                    start_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    end_date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_post", x => x.ID);
                    table.ForeignKey(
                        name: "FK__post__image_ID__47DBAE45",
                        column: x => x.image_ID,
                        principalTable: "image",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_transaction = table.Column<int>(nullable: true),
                    username = table.Column<string>(nullable: true),
                    date = table.Column<DateTime>(type: "datetime", nullable: true),
                    fulfilled = table.Column<bool>(nullable: true),
                    address = table.Column<string>(maxLength: 255, nullable: true),
                    city = table.Column<string>(maxLength: 255, nullable: true),
                    postal_code = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order", x => x.ID);
                    table.ForeignKey(
                        name: "FK__order__ID_transa__3E52440B",
                        column: x => x.ID_transaction,
                        principalTable: "transaction",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__order__username__3D5E1FD2",
                        column: x => x.username,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "article_availability",
                columns: table => new
                {
                    article_ID = table.Column<int>(nullable: false),
                    size = table.Column<string>(maxLength: 255, nullable: false),
                    quantity = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ArticleAvailability___CC37F2680A55149B", x => new { x.article_ID, x.size });
                    table.ForeignKey(
                        name: "FK__article_a__artic__403A8C7D",
                        column: x => x.article_ID,
                        principalTable: "article",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "discount",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    article_ID = table.Column<int>(nullable: false),
                    date = table.Column<DateTime>(type: "datetime", nullable: true),
                    start_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    end_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    percentage = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__discount__AED79301C419BEB6", x => new { x.ID, x.article_ID });
                    table.ForeignKey(
                        name: "FK__discount__articl__44FF419A",
                        column: x => x.article_ID,
                        principalTable: "article",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "review",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    article_ID = table.Column<int>(nullable: false),
                    date = table.Column<DateTime>(type: "datetime", nullable: true),
                    rating = table.Column<int>(nullable: true),
                    comment = table.Column<string>(maxLength: 255, nullable: true),
                    username = table.Column<string>(nullable: true),
                    blocked = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__review__AED793010ECF51E0", x => new { x.ID, x.article_ID });
                    table.ForeignKey(
                        name: "FK__review__article___45F365D3",
                        column: x => x.article_ID,
                        principalTable: "article",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__review___usern__03122019M43",
                        column: x => x.username,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "shopping_cart_article",
                columns: table => new
                {
                    shopping_cart_ID = table.Column<int>(nullable: false),
                    article_ID = table.Column<int>(nullable: false),
                    quantity = table.Column<int>(nullable: true),
                    size = table.Column<string>(maxLength: 255, nullable: true),
                    purchase_price = table.Column<decimal>(type: "decimal(18, 0)", nullable: true),
                    purchase_discount = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__shopping__B7E583C151B3360B", x => new { x.shopping_cart_ID, x.article_ID });
                    table.ForeignKey(
                        name: "FK__shopping___artic__4222D4EF",
                        column: x => x.article_ID,
                        principalTable: "article",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__shopping___shopp__4316F928",
                        column: x => x.shopping_cart_ID,
                        principalTable: "shopping_cart",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "order_article",
                columns: table => new
                {
                    order_ID = table.Column<int>(nullable: false),
                    article_ID = table.Column<int>(nullable: false),
                    quantity = table.Column<int>(nullable: true),
                    size = table.Column<string>(maxLength: 255, nullable: true),
                    purchase_price = table.Column<decimal>(type: "decimal(18, 0)", nullable: true),
                    purchase_discount = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__order_ar__DA851AC7823BC41D", x => new { x.order_ID, x.article_ID });
                    table.ForeignKey(
                        name: "FK__order_art__artic__3F466844",
                        column: x => x.article_ID,
                        principalTable: "article",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__order_art__order__412EB0B6",
                        column: x => x.order_ID,
                        principalTable: "order",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_article_image_ID",
                table: "article",
                column: "image_ID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_discount_article_ID",
                table: "discount",
                column: "article_ID");

            migrationBuilder.CreateIndex(
                name: "IX_order_ID_transaction",
                table: "order",
                column: "ID_transaction");

            migrationBuilder.CreateIndex(
                name: "IX_order_username",
                table: "order",
                column: "username");

            migrationBuilder.CreateIndex(
                name: "IX_order_article_article_ID",
                table: "order_article",
                column: "article_ID");

            migrationBuilder.CreateIndex(
                name: "IX_player_image_ID",
                table: "player",
                column: "image_ID");

            migrationBuilder.CreateIndex(
                name: "IX_post_image_ID",
                table: "post",
                column: "image_ID");

            migrationBuilder.CreateIndex(
                name: "IX_review_article_ID",
                table: "review",
                column: "article_ID");

            migrationBuilder.CreateIndex(
                name: "IX_review_username",
                table: "review",
                column: "username");

            migrationBuilder.CreateIndex(
                name: "IX_shopping_cart_username",
                table: "shopping_cart",
                column: "username");

            migrationBuilder.CreateIndex(
                name: "IX_shopping_cart_article_article_ID",
                table: "shopping_cart_article",
                column: "article_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "article_availability");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "discount");

            migrationBuilder.DropTable(
                name: "match");

            migrationBuilder.DropTable(
                name: "order_article");

            migrationBuilder.DropTable(
                name: "player");

            migrationBuilder.DropTable(
                name: "post");

            migrationBuilder.DropTable(
                name: "review");

            migrationBuilder.DropTable(
                name: "shopping_cart_article");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "order");

            migrationBuilder.DropTable(
                name: "article");

            migrationBuilder.DropTable(
                name: "shopping_cart");

            migrationBuilder.DropTable(
                name: "transaction");

            migrationBuilder.DropTable(
                name: "image");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
