using System.Linq.Expressions;
using System.Text.Json.Serialization;
using LearningEntityFramework;
using LearningEntityFramework.Endpoints;
using LearningEntityFramework.Entities;
using LearningEntityFramework.Mapping;
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
    option => option
        // .UseLazyLoadingProxies()
        .UseNpgsql(builder.Configuration.GetConnectionString("MyBoardsDbConnectionString"))
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
app.MapQueries();

app.Run();