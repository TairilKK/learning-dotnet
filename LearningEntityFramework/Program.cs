using LearningEntityFramework;
using LearningEntityFramework.Endpoints;
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
app.Run();