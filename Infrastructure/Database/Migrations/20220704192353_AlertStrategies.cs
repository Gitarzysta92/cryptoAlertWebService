using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    public partial class AlertStrategies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Strategies",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Strategies",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Alerts",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Strategies_UserId",
                table: "Strategies",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Strategies_Users_UserId",
                table: "Strategies",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Strategies_Users_UserId",
                table: "Strategies");

            migrationBuilder.DropIndex(
                name: "IX_Strategies_UserId",
                table: "Strategies");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Strategies");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Strategies");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Alerts");
        }
    }
}
