using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Merchee.DataAccess.Migrations
{
    public partial class AddedShelfAccessToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccessToken",
                table: "Shelf",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AccessToken",
                table: "CustomerShelfAction",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessToken",
                table: "Shelf");

            migrationBuilder.DropColumn(
                name: "AccessToken",
                table: "CustomerShelfAction");
        }
    }
}
