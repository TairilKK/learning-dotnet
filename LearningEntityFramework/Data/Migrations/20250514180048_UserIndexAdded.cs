using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningEntityFramework.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserIndexAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_Email_FullName",
                table: "Users",
                columns: new[] { "Email", "FullName" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Email_FullName",
                table: "Users");
        }
    }
}
