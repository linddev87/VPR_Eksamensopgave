using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Migrations
{
    public partial class ReplacedId2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShortId",
                table: "Assets",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Assets",
                newName: "ShortId");
        }
    }
}
