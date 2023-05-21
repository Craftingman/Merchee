using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Merchee.DataAccess.Migrations
{
    public partial class RevorkedShelfItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockTransaction");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "ShelfItem",
                newName: "QuantityAdded");

            migrationBuilder.RenameColumn(
                name: "ManufacturedAt",
                table: "ShelfItem",
                newName: "DateManufactured");

            migrationBuilder.RenameColumn(
                name: "TimeCreated",
                table: "CustomerShelfAction",
                newName: "Time");

            migrationBuilder.AddColumn<Guid>(
                name: "AddedByUserId",
                table: "ShelfItem",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateAdded",
                table: "ShelfItem",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateRemoved",
                table: "ShelfItem",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuantityRemoved",
                table: "ShelfItem",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RemovedByUserId",
                table: "ShelfItem",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShelfItem_AddedByUserId",
                table: "ShelfItem",
                column: "AddedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShelfItem_RemovedByUserId",
                table: "ShelfItem",
                column: "RemovedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShelfItem_AspNetUsers_AddedByUserId",
                table: "ShelfItem",
                column: "AddedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShelfItem_AspNetUsers_RemovedByUserId",
                table: "ShelfItem",
                column: "RemovedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShelfItem_AspNetUsers_AddedByUserId",
                table: "ShelfItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ShelfItem_AspNetUsers_RemovedByUserId",
                table: "ShelfItem");

            migrationBuilder.DropIndex(
                name: "IX_ShelfItem_AddedByUserId",
                table: "ShelfItem");

            migrationBuilder.DropIndex(
                name: "IX_ShelfItem_RemovedByUserId",
                table: "ShelfItem");

            migrationBuilder.DropColumn(
                name: "AddedByUserId",
                table: "ShelfItem");

            migrationBuilder.DropColumn(
                name: "DateAdded",
                table: "ShelfItem");

            migrationBuilder.DropColumn(
                name: "DateRemoved",
                table: "ShelfItem");

            migrationBuilder.DropColumn(
                name: "QuantityRemoved",
                table: "ShelfItem");

            migrationBuilder.DropColumn(
                name: "RemovedByUserId",
                table: "ShelfItem");

            migrationBuilder.RenameColumn(
                name: "QuantityAdded",
                table: "ShelfItem",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "DateManufactured",
                table: "ShelfItem",
                newName: "ManufacturedAt");

            migrationBuilder.RenameColumn(
                name: "Time",
                table: "CustomerShelfAction",
                newName: "TimeCreated");

            migrationBuilder.CreateTable(
                name: "StockTransaction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShelfProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockTransaction_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockTransaction_ShelfProduct_ShelfProductId",
                        column: x => x.ShelfProductId,
                        principalTable: "ShelfProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockTransaction_ShelfProductId",
                table: "StockTransaction",
                column: "ShelfProductId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransaction_UserId",
                table: "StockTransaction",
                column: "UserId");
        }
    }
}
