using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMedicalInventory.Migrations.MedicalInventoryDB
{
    /// <inheritdoc />
    public partial class UpdateTableRequestObat2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "RequestObat",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "RequestObat",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "RequestObat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "RequestObat",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "RequestObat");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "RequestObat");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "RequestObat");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "RequestObat");
        }
    }
}
