using Microsoft.EntityFrameworkCore.Migrations;

namespace RudesWebapp.Migrations
{
    public partial class MatchAndReviewFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK__review__AED793010ECF51E0",
                table: "review");

            migrationBuilder.AddPrimaryKey(
                name: "PK_review",
                table: "review",
                column: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_review",
                table: "review");

            migrationBuilder.AddPrimaryKey(
                name: "PK__review__AED793010ECF51E0",
                table: "review",
                columns: new[] { "ID", "article_ID" });
        }
    }
}
