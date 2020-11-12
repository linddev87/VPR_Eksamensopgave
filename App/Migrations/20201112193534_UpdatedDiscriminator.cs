using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Migrations
{
    public partial class UpdatedDiscriminator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Symbols");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Symbols",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
