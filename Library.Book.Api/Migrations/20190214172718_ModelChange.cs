using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Book.Api.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class ModelChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                "No",
                "BOOKS",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                "COVER_STATUS",
                "BOOKS",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                "GENRE",
                "BOOKS",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "No",
                "BOOKS");

            migrationBuilder.DropColumn(
                "COVER_STATUS",
                "BOOKS");

            migrationBuilder.DropColumn(
                "GENRE",
                "BOOKS");
        }
    }
}