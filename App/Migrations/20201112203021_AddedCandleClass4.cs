using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Migrations
{
    public partial class AddedCandleClass4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SymbolId",
                table: "Symbols",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CandleId",
                table: "Candles",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Symbols",
                newName: "SymbolId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Candles",
                newName: "CandleId");
        }
    }
}
