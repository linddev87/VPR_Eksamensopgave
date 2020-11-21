using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Migrations
{
    public partial class shortlistadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Shortlisted",
                table: "Assets",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Shortlisted",
                table: "Assets");
        }
    }
}
