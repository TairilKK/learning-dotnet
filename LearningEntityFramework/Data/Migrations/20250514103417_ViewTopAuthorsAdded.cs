using Microsoft.EntityFrameworkCore.Migrations;

namespace LearningEntityFramework.Data.Migrations
{
    public partial class ViewTopAuthorsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
CREATE VIEW View_TopAuthors as
SELECT u.""FullName"", count(*) as ""WorkItemsCount""
FROM ""Users"" u
JOIN public.""WorkItems"" WI on u.""Id"" = WI.""AuthorId""
GROUP BY u.""FullName"", u.""Id""
order by ""WorkItemsCount"" desc
limit 5
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
Drop VIEW View_TopAuthors
");
        }
    }
}
