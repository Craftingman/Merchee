using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Merchee.DataAccess.Migrations
{
    public partial class AddedUserRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessToken",
                table: "CustomerShelfAction");

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "AccessToken",
                table: "CustomerShelfAction",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
