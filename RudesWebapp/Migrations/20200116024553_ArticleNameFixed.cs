using Microsoft.EntityFrameworkCore.Migrations;

namespace RudesWebapp.Migrations
{
    public partial class ArticleNameFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_article_name",
                table: "article");

            migrationBuilder.CreateIndex(
                name: "IX_article_name",
                table: "article",
                column: "name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_article_name",
                table: "article");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_article_name",
                table: "article",
                column: "name");
        }
    }
}
