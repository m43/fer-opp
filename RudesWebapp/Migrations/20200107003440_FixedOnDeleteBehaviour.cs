using Microsoft.EntityFrameworkCore.Migrations;

namespace RudesWebapp.Migrations
{
    public partial class FixedOnDeleteBehaviour : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__article__image_I__46E78A0C",
                table: "article");

            migrationBuilder.DropForeignKey(
                name: "FK__article_a__artic__403A8C7D",
                table: "article_availability");

            migrationBuilder.DropForeignKey(
                name: "FK__discount__articl__44FF419A",
                table: "discount");

            migrationBuilder.DropForeignKey(
                name: "FK__order_a__article__3F466844",
                table: "order_article");

            migrationBuilder.DropForeignKey(
                name: "FK__order_art__order__412EB0B6",
                table: "order_article");

            migrationBuilder.DropForeignKey(
                name: "FK__review__article___45F365D3",
                table: "review");

            migrationBuilder.DropForeignKey(
                name: "FK__review___usern__03122019M43",
                table: "review");

            migrationBuilder.DropForeignKey(
                name: "FK__shopping___artic__4222D4EF",
                table: "shopping_cart_article");

            migrationBuilder.DropForeignKey(
                name: "FK__shopping___shopp__4316F928",
                table: "shopping_cart_article");

            migrationBuilder.DropPrimaryKey(
                name: "PK__order_ar__DA851AC7823BC41D",
                table: "order_article");

            migrationBuilder.AlterColumn<string>(
                name: "user_ID",
                table: "review",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<decimal>(
                name: "purchase_price",
                table: "order_article",
                type: "decimal(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 0)");

            migrationBuilder.AlterColumn<int>(
                name: "article_ID",
                table: "order_article",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "order_article",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<decimal>(
                name: "price",
                table: "article",
                type: "decimal(18, 2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_order_article",
                table: "order_article",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_order_article_order_ID",
                table: "order_article",
                column: "order_ID");

            migrationBuilder.AddForeignKey(
                name: "FK__article__image_I__46E78A0C",
                table: "article",
                column: "image_ID",
                principalTable: "image",
                principalColumn: "ID",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__article_a__artic__403A8C7D",
                table: "article_availability",
                column: "article_ID",
                principalTable: "article",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__discount__articl__44FF419A",
                table: "discount",
                column: "article_ID",
                principalTable: "article",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__order_a__article__3F466844",
                table: "order_article",
                column: "article_ID",
                principalTable: "article",
                principalColumn: "ID",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__order_art__order__412EB0B6",
                table: "order_article",
                column: "order_ID",
                principalTable: "order",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__review__article___45F365D3",
                table: "review",
                column: "article_ID",
                principalTable: "article",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__review___usern__03122019M43",
                table: "review",
                column: "user_ID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__shopping___artic__4222D4EF",
                table: "shopping_cart_article",
                column: "article_ID",
                principalTable: "article",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__shopping___shopp__4316F928",
                table: "shopping_cart_article",
                column: "shopping_cart_ID",
                principalTable: "shopping_cart",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__article__image_I__46E78A0C",
                table: "article");

            migrationBuilder.DropForeignKey(
                name: "FK__article_a__artic__403A8C7D",
                table: "article_availability");

            migrationBuilder.DropForeignKey(
                name: "FK__discount__articl__44FF419A",
                table: "discount");

            migrationBuilder.DropForeignKey(
                name: "FK__order_a__article__3F466844",
                table: "order_article");

            migrationBuilder.DropForeignKey(
                name: "FK__order_art__order__412EB0B6",
                table: "order_article");

            migrationBuilder.DropForeignKey(
                name: "FK__review__article___45F365D3",
                table: "review");

            migrationBuilder.DropForeignKey(
                name: "FK__review___usern__03122019M43",
                table: "review");

            migrationBuilder.DropForeignKey(
                name: "FK__shopping___artic__4222D4EF",
                table: "shopping_cart_article");

            migrationBuilder.DropForeignKey(
                name: "FK__shopping___shopp__4316F928",
                table: "shopping_cart_article");

            migrationBuilder.DropPrimaryKey(
                name: "PK_order_article",
                table: "order_article");

            migrationBuilder.DropIndex(
                name: "IX_order_article_order_ID",
                table: "order_article");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "order_article");

            migrationBuilder.AlterColumn<string>(
                name: "user_ID",
                table: "review",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "purchase_price",
                table: "order_article",
                type: "decimal(18, 0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 2)");

            migrationBuilder.AlterColumn<int>(
                name: "article_ID",
                table: "order_article",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "price",
                table: "article",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 2)");

            migrationBuilder.AddPrimaryKey(
                name: "PK__order_ar__DA851AC7823BC41D",
                table: "order_article",
                columns: new[] { "order_ID", "article_ID" });

            migrationBuilder.AddForeignKey(
                name: "FK__article__image_I__46E78A0C",
                table: "article",
                column: "image_ID",
                principalTable: "image",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__article_a__artic__403A8C7D",
                table: "article_availability",
                column: "article_ID",
                principalTable: "article",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__discount__articl__44FF419A",
                table: "discount",
                column: "article_ID",
                principalTable: "article",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__order_a__article__3F466844",
                table: "order_article",
                column: "article_ID",
                principalTable: "article",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__order_art__order__412EB0B6",
                table: "order_article",
                column: "order_ID",
                principalTable: "order",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__review__article___45F365D3",
                table: "review",
                column: "article_ID",
                principalTable: "article",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__review___usern__03122019M43",
                table: "review",
                column: "user_ID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__shopping___artic__4222D4EF",
                table: "shopping_cart_article",
                column: "article_ID",
                principalTable: "article",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__shopping___shopp__4316F928",
                table: "shopping_cart_article",
                column: "shopping_cart_ID",
                principalTable: "shopping_cart",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
