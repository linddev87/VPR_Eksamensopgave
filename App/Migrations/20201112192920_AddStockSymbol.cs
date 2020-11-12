using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Migrations
{
    public partial class AddStockSymbol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Symbols",
                newName: "SymbolType");

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Symbols",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Symbols",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StockType",
                table: "Symbols",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Symbols");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Symbols");

            migrationBuilder.DropColumn(
                name: "StockType",
                table: "Symbols");

            migrationBuilder.RenameColumn(
                name: "SymbolType",
                table: "Symbols",
                newName: "Type");
        }
    }
}
