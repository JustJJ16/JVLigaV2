using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JVLigaV2.Migrations
{
    public partial class FilePath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArticleImage",
                table: "Articles");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Articles",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Articles");

            migrationBuilder.AddColumn<byte[]>(
                name: "ArticleImage",
                table: "Articles",
                nullable: false,
                defaultValue: new byte[] {  });
        }
    }
}
