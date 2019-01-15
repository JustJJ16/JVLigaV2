using Microsoft.EntityFrameworkCore.Migrations;

namespace LeagueData.Migrations
{
    public partial class FullDbV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MatchId",
                table: "Articles",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Articles_MatchId",
                table: "Articles",
                column: "MatchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Matches_MatchId",
                table: "Articles",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Matches_MatchId",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_MatchId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "MatchId",
                table: "Articles");
        }
    }
}
