using Microsoft.EntityFrameworkCore.Migrations;

namespace RudesWebapp.Migrations
{
    public partial class PlayerTypeAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "player_type",
                table: "player",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "player_type",
                table: "player");
        }
    }
}
