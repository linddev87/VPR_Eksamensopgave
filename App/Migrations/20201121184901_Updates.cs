using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace App.Migrations
{
    public partial class Updates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "From",
                table: "Candles");

            migrationBuilder.RenameColumn(
                name: "To",
                table: "Candles",
                newName: "Timestamp");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Candles",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<decimal>(
                name: "ClosingPrice",
                table: "Candles",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "HighestPrice",
                table: "Candles",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "LowestPrice",
                table: "Candles",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OpenPrice",
                table: "Candles",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClosingPrice",
                table: "Candles");

            migrationBuilder.DropColumn(
                name: "HighestPrice",
                table: "Candles");

            migrationBuilder.DropColumn(
                name: "LowestPrice",
                table: "Candles");

            migrationBuilder.DropColumn(
                name: "OpenPrice",
                table: "Candles");

            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "Candles",
                newName: "To");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Candles",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<DateTime>(
                name: "From",
                table: "Candles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
