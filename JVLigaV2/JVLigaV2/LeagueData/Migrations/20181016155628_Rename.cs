using Microsoft.EntityFrameworkCore.Migrations;

namespace LeagueData.Migrations
{
    public partial class Rename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NAME",
                table: "Halls",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Halls",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Halls",
                newName: "NAME");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Halls",
                newName: "ID");
        }
    }
}
