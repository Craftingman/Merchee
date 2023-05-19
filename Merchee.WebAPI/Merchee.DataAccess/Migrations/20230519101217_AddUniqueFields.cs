using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Merchee.DataAccess.Migrations
{
    public partial class AddUniqueFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Shelf");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Shelf");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "ShelfProduct",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Barcode",
                table: "Shelf",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Product",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Barcode",
                table: "Product",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "CustomerShelfAction",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Company",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Shelf_Barcode",
                table: "Shelf",
                column: "Barcode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_Barcode",
                table: "Product",
                column: "Barcode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_Name",
                table: "Product",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Company_Name",
                table: "Company",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Shelf_Barcode",
                table: "Shelf");

            migrationBuilder.DropIndex(
                name: "IX_Product_Barcode",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_Name",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Company_Name",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "ShelfProduct");

            migrationBuilder.DropColumn(
                name: "Barcode",
                table: "Shelf");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "CustomerShelfAction");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Shelf",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Shelf",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Barcode",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Company",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
