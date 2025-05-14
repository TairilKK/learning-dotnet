using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningEntityFramework.Data.Migrations
{
    /// <inheritdoc />
    public partial class CoordnatesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Coordinate_Latitude",
                table: "Addresses",
                type: "numeric(18,7)",
                precision: 18,
                scale: 7,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Coordinate_Longitude",
                table: "Addresses",
                type: "numeric(18,7)",
                precision: 18,
                scale: 7,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Coordinate_Latitude",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Coordinate_Longitude",
                table: "Addresses");
        }
    }
}
