using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CCP.Repositori.Migrations
{
    /// <inheritdoc />
    public partial class changeAlot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Appointments",
                newName: "ParentId");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "SleepPatterns",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "PhysicalActivities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "NutritionalIntakes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Measurements",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MeasurementId",
                table: "Appointments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "NutritionalIntake",
                table: "Appointments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PhysicalActivityId",
                table: "Appointments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SleepPatternId",
                table: "Appointments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "SleepPatterns");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "PhysicalActivities");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "NutritionalIntakes");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "MeasurementId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "NutritionalIntake",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "PhysicalActivityId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "SleepPatternId",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "Appointments",
                newName: "UserId");
        }
    }
}
