using LearningMinimalAPI.Data;
using LearningMinimalAPI.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("GameStore");
builder.Services.AddSqlite<GameStoreContext>(connString);

var app = builder.Build();

await app.MigrateDatabaseAsync();

// GET: /
app.MapGet("/", () => Results.Ok("Welcome in LearningMinimalAPI project"));

app.MapGamesEndpoints();
app.MapGenresEndpoints();

app.Run();