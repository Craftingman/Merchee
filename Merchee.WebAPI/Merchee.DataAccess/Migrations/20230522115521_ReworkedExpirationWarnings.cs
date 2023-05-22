using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Merchee.DataAccess.Migrations
{
    public partial class ReworkedExpirationWarnings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpirationWarning_ShelfItem_ShelfItemId",
                table: "ExpirationWarning");

            migrationBuilder.RenameColumn(
                name: "ShelfItemId",
                table: "ExpirationWarning",
                newName: "ShelfProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ExpirationWarning_ShelfItemId",
                table: "ExpirationWarning",
                newName: "IX_ExpirationWarning_ShelfProductId");

            migrationBuilder.AddColumn<int>(
                name: "FullCapacity",
                table: "ShelfProduct",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ProductDateManufactured",
                table: "ExpirationWarning",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_ExpirationWarning_ShelfProduct_ShelfProductId",
                table: "ExpirationWarning",
                column: "ShelfProductId",
                principalTable: "ShelfProduct",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpirationWarning_ShelfProduct_ShelfProductId",
                table: "ExpirationWarning");

            migrationBuilder.DropColumn(
                name: "FullCapacity",
                table: "ShelfProduct");

            migrationBuilder.DropColumn(
                name: "ProductDateManufactured",
                table: "ExpirationWarning");

            migrationBuilder.RenameColumn(
                name: "ShelfProductId",
                table: "ExpirationWarning",
                newName: "ShelfItemId");

            migrationBuilder.RenameIndex(
                name: "IX_ExpirationWarning_ShelfProductId",
                table: "ExpirationWarning",
                newName: "IX_ExpirationWarning_ShelfItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpirationWarning_ShelfItem_ShelfItemId",
                table: "ExpirationWarning",
                column: "ShelfItemId",
                principalTable: "ShelfItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
