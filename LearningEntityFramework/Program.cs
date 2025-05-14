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

app.MapGet("pagination", async (MyBoardsContext db) =>
{
    var filter = "a";
    var sortBy = "FullName"; // "FullName" "Email" null
    var sortByDescending = false;
    var pageNumber = 1;
    var pageSize = 10;
    filter = filter.ToLower();
    var query = db.Users
        .Where(u => filter == null ||
            u.Email.ToLower().Contains(filter) ||
            u.FullName.ToLower().Contains(filter));

    var totalCount = query.Count();

    if (sortBy is not null)
    {
        var columnsSelector = new Dictionary<string, Expression<Func<User, object>>>()
        {
            { nameof(User.Email), user => user.Email },
            { nameof(User.FullName), user => user.FullName }
        };

        var sortByExpression = columnsSelector[sortBy];
        query = sortByDescending
                ? query.OrderByDescending(sortByExpression)
                : query.OrderBy(sortByExpression);
    }

    query = query.Skip((pageNumber - 1) * pageSize)
        .Take(pageSize);

    var result =  await query.ToListAsync();

    return result.ToPagedResultDto(totalCount, pageSize, pageNumber);
});

app.Run();