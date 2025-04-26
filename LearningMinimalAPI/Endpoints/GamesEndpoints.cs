using LearningMinimalAPI.Dtos;

namespace LearningMinimalAPI.Endpoints;

public static class GamesEndpoints
{
    private readonly static List<GameDto> games =
    [
        new (1, "Tekken 8", "Fighting", 55.19M, new DateOnly(2024, 1, 26)),
        new (2, "Grand Theft Auto V","Action-adventure", 15.99M, new DateOnly(2013, 9, 17)),
        new (3, "Rocket League","Sports", 0M, new DateOnly(2015, 7, 7)),
    ];


    const string getGameEndpointName = "GetGame";

    public static WebApplication MapGamesEndpoints(this WebApplication app)
    {
        //GET: /games
        app.MapGet("games", () => games);

        //GET /games/id
        app.MapGet("games/{id}", (int id) =>
        {
            var game = games.Find(game => game.Id == id);
            return game is null ? Results.NotFound() : Results.Ok(game);
        }).WithName(getGameEndpointName);

        app.MapPost("games", (CreateGameDto newGame) =>
        {
            GameDto game = new(
                games.Count + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
            );

            games.Add(game);
            return Results.CreatedAtRoute(getGameEndpointName, new { id = game.Id }, game);
        });

        // PUT: /games/id
        app.MapPut("games/{id}", (int id, UpdateGameDto updatedGame) =>
        {
            var index = games.FindIndex(game => game.Id == id);
            if (index == -1)
            {
                return Results.NotFound();
            }

            games[index] = new GameDto(
                index,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate
            );
            return Results.NoContent();
        });

        // DELETE: /games/id
        app.MapDelete("games/{id}", (int id) =>
        {
            games.RemoveAll(game => game.Id == id);
            return Results.NoContent();
        });

        return app;
    }
}