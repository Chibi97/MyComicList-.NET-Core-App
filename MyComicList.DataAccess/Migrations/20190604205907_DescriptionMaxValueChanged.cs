using Microsoft.EntityFrameworkCore.Migrations;

namespace MyComicList.DataAccess.Migrations
{
    public partial class DescriptionMaxValueChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Comics",
                maxLength: 700,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 200);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Comics",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 700);
        }
    }
}
