using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class UpdateRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Users_TeamLeaderId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_TeamLeaderId",
                table: "Teams");

            migrationBuilder.AlterColumn<string>(
                name: "TeamLeaderId",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeamLeaderId1",
                table: "Teams",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_TeamLeaderId1",
                table: "Teams",
                column: "TeamLeaderId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Users_TeamLeaderId1",
                table: "Teams",
                column: "TeamLeaderId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Users_TeamLeaderId1",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_TeamLeaderId1",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "TeamLeaderId1",
                table: "Teams");

            migrationBuilder.AlterColumn<string>(
                name: "TeamLeaderId",
                table: "Teams",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_TeamLeaderId",
                table: "Teams",
                column: "TeamLeaderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Users_TeamLeaderId",
                table: "Teams",
                column: "TeamLeaderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
