using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Book.Api.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class Initials : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "BOOKS",
                table => new
                         {
                             ID = table.Column<int>(nullable: false)
                                 .Annotation("SqlServer:ValueGenerationStrategy"
                                             , SqlServerValueGenerationStrategy.IdentityColumn)
                             , CREATED = table.Column<DateTime>("datetime2", nullable: false)
                             , UPDATED = table.Column<DateTime>("datetime2", nullable: false)
                             , CREATED_BY = table.Column<string>(nullable: true)
                             , UPDATED_BY = table.Column<string>(nullable: true)
                             , USER_ID = table.Column<int>(nullable: false), NAME = table.Column<string>(nullable: true)
                             , PUBLISHER_ID = table.Column<int>(nullable: false)
                             , AUTHOR_ID = table.Column<int>(nullable: false)
                             , PUBLISH_DATE = table.Column<DateTime>(nullable: false)
                             , LIBRARY_ID = table.Column<int>(nullable: false)
                         },
                constraints: table => { table.PrimaryKey("PK_BOOKS", x => x.ID); });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "BOOKS");
        }
    }
}