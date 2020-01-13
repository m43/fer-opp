using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RudesWebapp.Migrations
{
    public partial class OrderUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__order__user__3D5E1FD2",
                table: "order");

            migrationBuilder.AlterColumn<string>(
                name: "user_ID",
                table: "order",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<DateTime>(
                name: "last_modification_date",
                table: "order",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "user_who_made_last_modifications_email",
                table: "order",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK__order__user__3D5E1FD2",
                table: "order",
                column: "user_ID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__order__user__3D5E1FD2",
                table: "order");

            migrationBuilder.DropColumn(
                name: "last_modification_date",
                table: "order");

            migrationBuilder.DropColumn(
                name: "user_who_made_last_modifications_email",
                table: "order");

            migrationBuilder.AlterColumn<string>(
                name: "user_ID",
                table: "order",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK__order__user__3D5E1FD2",
                table: "order",
                column: "user_ID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
