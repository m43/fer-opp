using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RudesWebapp.Migrations
{
    public partial class CustomUserAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__order__username__3D5E1FD2",
                table: "order");

            migrationBuilder.DropForeignKey(
                name: "FK__shopping___usern__440B1D61",
                table: "shopping_cart");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropColumn(
                name: "adress",
                table: "order");

            migrationBuilder.AlterColumn<string>(
                name: "username",
                table: "shopping_cart",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "username",
                table: "review",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "username",
                table: "order",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "address",
                table: "order",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationDate",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK__order__username__3D5E1FD2",
                table: "order",
                column: "username",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__shopping___usern__440B1D61",
                table: "shopping_cart",
                column: "username",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__order__username__3D5E1FD2",
                table: "order");

            migrationBuilder.DropForeignKey(
                name: "FK__shopping___usern__440B1D61",
                table: "shopping_cart");

            migrationBuilder.DropColumn(
                name: "address",
                table: "order");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RegistrationDate",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "username",
                table: "shopping_cart",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "username",
                table: "review",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "username",
                table: "order",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "adress",
                table: "order",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    username = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    last_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    password_hash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    phone_number = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    registration_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    role = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__user__F3DBC57307D3AF22", x => x.username);
                });

            migrationBuilder.CreateIndex(
                name: "UQ__user__AB6E61640B9CFCFF",
                table: "user",
                column: "email",
                unique: true,
                filter: "[email] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK__order__username__3D5E1FD2",
                table: "order",
                column: "username",
                principalTable: "user",
                principalColumn: "username",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__shopping___usern__440B1D61",
                table: "shopping_cart",
                column: "username",
                principalTable: "user",
                principalColumn: "username",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
