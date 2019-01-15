using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LeagueData.Migrations
{
    public partial class AddAddresses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ADDRESS_ID",
                table: "Halls");

            migrationBuilder.AddColumn<int>(
                name: "AddressID",
                table: "Halls",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    STREET = table.Column<string>(nullable: true),
                    ZIP_CODE = table.Column<int>(nullable: false),
                    CITY = table.Column<string>(nullable: true),
                    STATE = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Halls_AddressID",
                table: "Halls",
                column: "AddressID");

            migrationBuilder.AddForeignKey(
                name: "FK_Halls_Addresses_AddressID",
                table: "Halls",
                column: "AddressID",
                principalTable: "Addresses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Halls_Addresses_AddressID",
                table: "Halls");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Halls_AddressID",
                table: "Halls");

            migrationBuilder.DropColumn(
                name: "AddressID",
                table: "Halls");

            migrationBuilder.AddColumn<int>(
                name: "ADDRESS_ID",
                table: "Halls",
                nullable: false,
                defaultValue: 0);
        }
    }
}
