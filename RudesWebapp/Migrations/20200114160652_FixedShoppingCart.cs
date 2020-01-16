using Microsoft.EntityFrameworkCore.Migrations;

namespace RudesWebapp.Migrations
{
    public partial class FixedShoppingCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_shopping_cart_user_ID",
                table: "shopping_cart");

            migrationBuilder.CreateIndex(
                name: "IX_shopping_cart_user_ID",
                table: "shopping_cart",
                column: "user_ID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_shopping_cart_user_ID",
                table: "shopping_cart");

            migrationBuilder.CreateIndex(
                name: "IX_shopping_cart_user_ID",
                table: "shopping_cart",
                column: "user_ID");
        }
    }
}
