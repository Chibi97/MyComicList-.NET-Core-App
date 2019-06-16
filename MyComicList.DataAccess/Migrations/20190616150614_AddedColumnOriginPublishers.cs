using Microsoft.EntityFrameworkCore.Migrations;

namespace MyComicList.DataAccess.Migrations
{
    public partial class AddedColumnOriginPublishers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Origin",
                table: "Publishers",
                maxLength: 50,
                nullable: false
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Origin",
                table: "Publishers");
        }
    }
}
