using Microsoft.EntityFrameworkCore.Migrations;

namespace RudesWebapp.Migrations
{
    public partial class PlayerPositionTypeAndArticleColorAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "position",
                table: "player",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "color",
                table: "article",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "color",
                table: "article");

            migrationBuilder.AlterColumn<string>(
                name: "position",
                table: "player",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(int),
                oldMaxLength: 30);
        }
    }
}
