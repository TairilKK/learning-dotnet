using LearningMinimalAPI.Dtos;

namespace LearningMinimalAPI.Endpoints;

public static class GamesEndpoints
{
    private static readonly List<GameDto> Games =
    [
        new (1, "Tekken 8", "Fighting", 55.19M, new DateOnly(2024, 1, 26)),
        new (2, "Grand Theft Auto V","Action-adventure", 15.99M, new DateOnly(2013, 9, 17)),
        new (3, "Rocket League","Sports", 0M, new DateOnly(2015, 7, 7)),
    ];

    const string GetGameEndpointName = "GetGame";

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games")
            .WithParameterValidation();

        //GET: /games
        group.MapGet("/", () => Games);

        //GET /games/id
        group.MapGet("/{id}", (int id) =>
        {
            var game = Games.Find(game => game.Id == id);
            return game is null ? Results.NotFound() : Results.Ok(game);
        }).WithName(GetGameEndpointName);

        group.MapPost("/", (CreateGameDto newGame) =>
        {
            GameDto game = new(
                Games.Count + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
            );

            Games.Add(game);
            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
        });

        // PUT: /games/id
        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
        {
            var index = Games.FindIndex(game => game.Id == id);
            if (index == -1)
            {
                return Results.NotFound();
            }

            Games[index] = new GameDto(
                index,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate
            );
            return Results.NoContent();
        });

        // DELETE: /games/id
        group.MapDelete("/{id}", (int id) =>
        {
            Games.RemoveAll(game => game.Id == id);
            return Results.NoContent();
        });

        return group;
    }
}