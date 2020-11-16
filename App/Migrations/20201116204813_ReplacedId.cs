using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Migrations
{
    public partial class ReplacedId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Assets",
                table: "Assets");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Assets",
                newName: "Guid");

            migrationBuilder.AddColumn<string>(
                name: "ShortId",
                table: "Assets",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assets",
                table: "Assets",
                column: "ShortId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Assets",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "ShortId",
                table: "Assets");

            migrationBuilder.RenameColumn(
                name: "Guid",
                table: "Assets",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assets",
                table: "Assets",
                column: "Id");
        }
    }
}
