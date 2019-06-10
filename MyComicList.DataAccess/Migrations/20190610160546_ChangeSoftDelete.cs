using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyComicList.DataAccess.Migrations
{
    public partial class ChangeSoftDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "MyList");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "ComicGenres");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "ComicAuthors");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Comics",
                maxLength: 700,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 500);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Reviews",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "MyList",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Comics",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 700);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "ComicGenres",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "ComicAuthors",
                nullable: true);
        }
    }
}
