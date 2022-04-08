using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddApprovalStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Vacations");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Vacations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Vacations");

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Vacations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
