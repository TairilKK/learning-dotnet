using LearningEntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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

var users = dbContext.Users.ToList();
if (!users.Any())
{
    var user1 = new User()
    {
        Email = "user1@email.com",
        FullName = "User One",
        Address = new Address()
        {
            City = "Warszawa",
            Street = "Szeroka"
        }
    };


    var user2 = new User()
    {
        Email = "user2@email.com",
        FullName = "User Two",
        Address = new Address()
        {
            City = "Kraków",
            Street = "Szeroka"
        }
    };

    dbContext.Users.AddRange(user1, user2);
    dbContext.SaveChanges();
}

var tags = dbContext.Tags.ToList();
if (!tags.Any())
{
    var newTags = new List<Tag>
    {
        new Tag() { Value = "Web" },
        new Tag() { Value = "UI" },
        new Tag() { Value = "Desktop" },
        new Tag() { Value = "API" },
        new Tag() { Value = "Service" },
    };

    dbContext.Tags.AddRange(newTags);
    dbContext.SaveChanges();
}

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

app.Run();