using LearningEntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningEntityFramework.Endpoints;

public static class QueriesEndpoints
{
    public static void MapSimpleQueries(this WebApplication app)
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
    }
}