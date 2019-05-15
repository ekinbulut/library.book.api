using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Book.Api.Migrations
{
    public partial class ModelChangeColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                "No",
                "BOOKS",
                "NO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                "NO",
                "BOOKS",
                "No");
        }
    }
}