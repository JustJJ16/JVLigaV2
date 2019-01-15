using Microsoft.EntityFrameworkCore.Migrations;

namespace LeagueData.Migrations
{
    public partial class UserRole3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRolse",
                table: "UserRolse");

            migrationBuilder.RenameTable(
                name: "UserRolse",
                newName: "UserRoles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles");

            migrationBuilder.RenameTable(
                name: "UserRoles",
                newName: "UserRolse");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRolse",
                table: "UserRolse",
                column: "Id");
        }
    }
}
