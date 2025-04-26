using LearningMinimalAPI.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();
app.MapGamesEndpoints();

// GET: /
app.MapGet("/", () => Results.Ok("Welcome in LearningMinimalAPI project"));

app.Run();