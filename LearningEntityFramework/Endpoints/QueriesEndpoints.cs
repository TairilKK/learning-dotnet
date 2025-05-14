using LearningEntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningEntityFramework.Endpoints;

public static class QueriesEndpoints
{
    public static void MapQueries(this WebApplication app)
    {
        app.MapGet("tags", (MyBoardsContext db) => db.Tags.ToList());
        app.MapGet("firstEpicFirstUserOne", (MyBoardsContext db) =>
        {
            var epic = db.Epics.First();
            var user = db.Users.First(u => u.FullName == "User One");
            return new { epic, user };
        });
        app.MapGet("toDoWorkItems", (MyBoardsContext db) =>
        {
            return db.WorkItems.Where(wi => wi.StateId == 1).ToList();
        });
        app.MapGet("newComments", async (MyBoardsContext db) =>
        {
            return await db
                .Comments
                .OrderByDescending(c => c.CreatedDate)
                .Take(5)
                .ToListAsync();
        });
        app.MapGet("workItemCounts", async (MyBoardsContext db) =>
        {
            return await db
                .WorkItems
                .GroupBy(wi => wi.StateId)
                .Select(g => new {stateId = g.Key, count = g.Count()})
                .ToListAsync();
        });
        app.MapGet("epicOnHold", async (MyBoardsContext db) =>
        {
            return await db
                .Epics
                .Where(e => e.StateId==4)
                .OrderBy(e => e.Priority)
                .ToListAsync();
        });
        app.MapGet("authorWithMostComment", async (MyBoardsContext db) =>
        {
            var authorWithMostComment = await db
                .Comments
                .GroupBy(c => c.AuthorId)
                .Select(c => new {AuthorId = c.Key, count = c.Count()})
                .OrderByDescending(c => c.count)
                .FirstAsync();

            var authorDetails = await db
                .Users
                .Where(u => u.Id == authorWithMostComment.AuthorId)
                .FirstAsync();

            return new { authorDetails, authorWithMostComment.count };
        });

        app.MapPut("updateAreaPriority", async (MyBoardsContext db) =>
        {
            var epic = await db.Epics.FirstAsync(epic => epic.Id == 1);

            epic.Area = "Updated area";
            epic.Priority = 1;

            await db.SaveChangesAsync();

            return epic;
        });

        app.MapPut("updateState", async (MyBoardsContext db) =>
        {
            var epic = await db.Epics.FirstAsync(epic => epic.Id == 1);

            epic.StateId = 1;

            await db.SaveChangesAsync();

            return epic;
        });

        app.MapPost("createTag", async (MyBoardsContext db) =>
        {
            var tag = new Tag()
            {
                Value = "EF"
            };

            // await db.AddAsync(tag);
            await db.Tags.AddAsync(tag);

            await db.SaveChangesAsync();
            return tag;
        });

        app.MapDelete("deleteTag", async (MyBoardsContext db) =>
        {
            var tag = await db.Tags
                .Where(t => t.Value == "EF")
                .FirstAsync();

            if (tag is null)
            {
                return;
            }

            db.Tags.Remove(tag);
            await db.SaveChangesAsync();
        });

        app.MapGet("getUserComments", async (MyBoardsContext db) =>
        {
            var user = await db.Users
                .Include(u => u.Comments).ThenInclude(c=>c.WorkItem)
                .Include(u=>u.Address)
                .FirstAsync(u => u.Id == Guid.Parse("68366DBE-0809-490F-CC1D-08DA10AB0E61"));

            // var userComments = await db.Comments.Where(c => c.AuthorId == user.Id).ToListAsync();
            return user;
        });
        app.MapGet("getRawSql", async (MyBoardsContext db) =>
        {
            var minWorkItemsCount = 85;
            var workItems = await db.WorkItemStates
                .FromSqlInterpolated($@"
SELECT wis.""Id"", wis.""Value""
FROM ""WorkItemStates"" wis
JOIN ""WorkItems"" wi on wi.""StateId"" = wis.""Id""
GROUP BY wis.""Id"", wis.""Value""
HAVING Count(*) > {minWorkItemsCount}
        ").ToListAsync();

            return workItems;
        });

        app.MapGet("getDataFromView", async (MyBoardsContext db)
            => await db.ViewTopAuthors.ToListAsync()
        );

        app.MapGet("getLazyLoading", async (MyBoardsContext db) =>
        {
            var withAddress = true;
            var user = db.Users
                .First();

            if (withAddress)
            {
                var result = new
                {
                    FullName = user.FullName,
                    Address = $"{user.Address.Street} {user.Address.City}"
                };
                return result;
            }
            return new {
                FullName = user.FullName,
                Address = "-"
            };
        });
    }
}