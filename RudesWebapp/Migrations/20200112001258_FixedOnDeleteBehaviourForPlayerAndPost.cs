using Microsoft.EntityFrameworkCore.Migrations;

namespace RudesWebapp.Migrations
{
    public partial class FixedOnDeleteBehaviourForPlayerAndPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__player__image_ID__03122019M43",
                table: "player");

            migrationBuilder.DropForeignKey(
                name: "FK__post__image_ID__47DBAE45",
                table: "post");

            migrationBuilder.AddForeignKey(
                name: "FK__player__image_ID__03122019M43",
                table: "player",
                column: "image_ID",
                principalTable: "image",
                principalColumn: "ID",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__post__image_ID__47DBAE45",
                table: "post",
                column: "image_ID",
                principalTable: "image",
                principalColumn: "ID",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__player__image_ID__03122019M43",
                table: "player");

            migrationBuilder.DropForeignKey(
                name: "FK__post__image_ID__47DBAE45",
                table: "post");

            migrationBuilder.AddForeignKey(
                name: "FK__player__image_ID__03122019M43",
                table: "player",
                column: "image_ID",
                principalTable: "image",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__post__image_ID__47DBAE45",
                table: "post",
                column: "image_ID",
                principalTable: "image",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
