using Microsoft.EntityFrameworkCore.Migrations;

namespace LeagueData.Migrations
{
    public partial class articleDesc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Dexcription",
                table: "Articles",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dexcription",
                table: "Articles");
        }
    }
}
