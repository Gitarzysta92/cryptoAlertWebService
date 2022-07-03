using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    public partial class MultipleCoinThemes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coins_ColorThemes_ColorThemeId",
                table: "Coins");

            migrationBuilder.DropIndex(
                name: "IX_Coins_ColorThemeId",
                table: "Coins");

            migrationBuilder.DropColumn(
                name: "ColorThemeId",
                table: "Coins");

            migrationBuilder.AddColumn<int>(
                name: "CoinId",
                table: "ColorThemes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "ColorThemes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ColorThemes_CoinId",
                table: "ColorThemes",
                column: "CoinId");

            migrationBuilder.AddForeignKey(
                name: "FK_ColorThemes_Coins_CoinId",
                table: "ColorThemes",
                column: "CoinId",
                principalTable: "Coins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ColorThemes_Coins_CoinId",
                table: "ColorThemes");

            migrationBuilder.DropIndex(
                name: "IX_ColorThemes_CoinId",
                table: "ColorThemes");

            migrationBuilder.DropColumn(
                name: "CoinId",
                table: "ColorThemes");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "ColorThemes");

            migrationBuilder.AddColumn<int>(
                name: "ColorThemeId",
                table: "Coins",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Coins_ColorThemeId",
                table: "Coins",
                column: "ColorThemeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Coins_ColorThemes_ColorThemeId",
                table: "Coins",
                column: "ColorThemeId",
                principalTable: "ColorThemes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
