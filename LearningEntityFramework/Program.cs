using System.Text.Json.Serialization;
using LearningEntityFramework;
using LearningEntityFramework.Endpoints;
using LearningEntityFramework.Entities;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddDbContext<MyBoardsContext>(
    option => option.UseNpgsql(builder.Configuration.GetConnectionString("MyBoardsDbConnectionString"))
);
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetService<MyBoardsContext>();

var pendingMigrations = dbContext.Database.GetPendingMigrations();
if (pendingMigrations.Any())
{
    dbContext.Database.Migrate();
}

app.AddUserData(dbContext);
app.AddTagData(dbContext);

app.MapSimpleQueries();

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
app.Run();