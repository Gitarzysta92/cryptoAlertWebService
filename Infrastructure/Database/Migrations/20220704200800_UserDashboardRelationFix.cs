using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    public partial class UserDashboardRelationFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Dashboards_DashboardId",
                table: "Users");
            
            migrationBuilder.DropIndex(
                name: "IX_Users_DashboardId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DashboardId",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DashboardId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_DashboardId",
                table: "Users",
                column: "DashboardId");
        }
    }
}
