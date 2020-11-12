using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Migrations
{
    public partial class AddedCandleClass3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "_createdDate",
                table: "Candles",
                newName: "CreatedDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Candles",
                newName: "_createdDate");
        }
    }
}
