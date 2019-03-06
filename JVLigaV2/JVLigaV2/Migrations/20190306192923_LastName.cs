using Microsoft.EntityFrameworkCore.Migrations;

namespace JVLigaV2.Migrations
{
    public partial class LastName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SecondName",
                table: "Players",
                newName: "LastName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Players",
                newName: "SecondName");
        }
    }
}
