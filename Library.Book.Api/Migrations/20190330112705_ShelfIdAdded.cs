using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Book.Api.Migrations
{
    public partial class ShelfIdAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                "LIBRARY_ID",
                "BOOKS",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                "AUTHOR_ID",
                "BOOKS",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                "SHELF_NUMBER",
                "BOOKS",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "SHELF_NUMBER",
                "BOOKS");

            migrationBuilder.AlterColumn<int>(
                "LIBRARY_ID",
                "BOOKS",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                "AUTHOR_ID",
                "BOOKS",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}