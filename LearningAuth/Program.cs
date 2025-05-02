// dotnet user-jwts create

using System.Security.Claims;

Dictionary<string, List<string>> gamesMap = new()
{
    { "player1", new List<string>() { "Tekken 8", "GTA V" } },
    { "player2", new List<string>() { "Tekken 7", "Forza Horizon 5" } },
};

Dictionary<string, List<string>> subscriptionMap = new()
{
    { "silver", new List<string>() { "Tekken 7", "GTA V" } },
    { "gold", new List<string>() { "Tekken 7", "GTA V", "Tekken 8", "Forza Horizon 5" } },
};
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();

var app = builder.Build();

// dotnet user-jwts create --role admin
app.MapGet("/playergames", () => gamesMap)
    .RequireAuthorization(policy =>
    {
        policy.RequireRole("admin");
    });

// dotnet user-jwts create --role player -n player1
app.MapGet("/mygames", (ClaimsPrincipal user) =>
{
    var hasClaim = user.HasClaim(claim => claim.Type == "subscription");
    if (hasClaim)
    {
        var subs = user.FindFirstValue("subscription")
                   ?? throw new Exception("Claim has no value");
        return Results.Ok(subscriptionMap[subs]);
    }
    ArgumentNullException.ThrowIfNull(user.Identity?.Name);
    var username = user.Identity.Name;
    if (!gamesMap.ContainsKey(username))
    {
        return Results.Empty;
    }
    return Results.Ok(gamesMap[username]);
})
.RequireAuthorization(policy =>
{
    policy.RequireRole("player");
});
app.Run();