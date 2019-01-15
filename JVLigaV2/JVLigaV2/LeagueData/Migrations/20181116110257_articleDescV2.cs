using Microsoft.EntityFrameworkCore.Migrations;

namespace LeagueData.Migrations
{
    public partial class articleDescV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Dexcription",
                table: "Articles",
                newName: "Description");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Articles",
                newName: "Dexcription");
        }
    }
}
