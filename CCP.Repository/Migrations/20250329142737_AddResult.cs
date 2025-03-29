using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CCP.Repositori.Migrations
{
    /// <inheritdoc />
    public partial class AddResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SleepQualityRating",
                table: "SleepPatterns",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BMIResult",
                table: "Measurements",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BMIResultRaing",
                table: "Measurements",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeadCircumferenceResult",
                table: "Measurements",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeadCircumferenceResultRating",
                table: "Measurements",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeightResult",
                table: "Measurements",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeightResultRating",
                table: "Measurements",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WeightResult",
                table: "Measurements",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WeightResultRating",
                table: "Measurements",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SleepQualityRating",
                table: "SleepPatterns");

            migrationBuilder.DropColumn(
                name: "BMIResult",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "BMIResultRaing",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "HeadCircumferenceResult",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "HeadCircumferenceResultRating",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "HeightResult",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "HeightResultRating",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "WeightResult",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "WeightResultRating",
                table: "Measurements");
        }
    }
}
