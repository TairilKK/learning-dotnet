using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningEntityFramework.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedTime",
                table: "Comments",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "CreatedTime",
                table: "Comments",
                newName: "CreatedDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "Comments",
                newName: "UpdatedTime");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Comments",
                newName: "CreatedTime");
        }
    }
}
