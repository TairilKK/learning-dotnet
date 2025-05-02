// dotnet user-jwts create
Dictionary<string, List<string>> gamesMap = new()
{
    { "player1", new List<string>() { "Tekken 8", "GTA V" } },
    { "player2", new List<string>() { "Tekken 7", "Forza Horizon 5" } },
};

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();

var app = builder.Build();

app.MapGet("/playergames", () => gamesMap)
    .RequireAuthorization();

app.Run();