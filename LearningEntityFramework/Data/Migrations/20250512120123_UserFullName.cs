using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningEntityFramework.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserFullName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.Sql(@"
                UPDATE public.""Users""
                SET ""FullName"" = ""FirstName"" || ' ' || ""LastName""
            ");


            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.Sql(@"
                UPDATE public.""Users""
                SET 
                    ""FirstName"" = split_part(""FullName"", ' ', 1),
                    ""LastName"" = split_part(""FullName"", ' ', 2)
            ");


            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Users");

        }
    }
}
