using LearningMinimalAPI.Data;
using LearningMinimalAPI.Dtos;
using LearningMinimalAPI.Entities;
using LearningMinimalAPI.Mapping;
using Microsoft.EntityFrameworkCore;

namespace LearningMinimalAPI.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games")
            .WithParameterValidation();

        //GET: /games
        group.MapGet("/", async (GameStoreContext dbContext) =>
            await dbContext.Games
                .Include(game=>game.Genre)
                .Select(game => game.ToGameSummaryDto())
                .AsNoTracking()
                .ToListAsync()
        );

        //GET: /games/id
        group.MapGet("/{id:int}", async (int id, GameStoreContext dbContext) =>
        {
            Game? game = await dbContext.Games.FindAsync(id);

            return game is null ?
                Results.NotFound() : Results.Ok(game.ToGameDetailsDto());

        }).WithName(GetGameEndpointName);

        //POST: /games
        group.MapPost("/", async (CreateGameDto newGame, GameStoreContext dbContext) =>
        {
            Game game = newGame.ToEntity();
            game.Genre = dbContext.Genres.Find(newGame.GenreId);

            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();

            return Results.CreatedAtRoute(
                GetGameEndpointName,
                new { id = game.Id },
                game.ToGameSummaryDto());
        });

        // PUT: /games/id
        group.MapPut("/{id:int}", async (int id, UpdateGameDto updatedGame, GameStoreContext dbContext) =>
        {
            var existingGame = await dbContext.Games.FindAsync(id);
            if (existingGame is null)
            {
                return Results.NotFound();
            }

            dbContext.Entry(existingGame)
                .CurrentValues
                .SetValues(updatedGame.ToEntity(id));
            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        // DELETE: /games/id
        group.MapDelete("/{id:int}", async (int id, GameStoreContext dbContext) =>
        {
            var existingGame = await dbContext.Games.FindAsync(id);
            if (existingGame is not null)
            {
                dbContext.Games
                    .Where(game => game.Id == id)
                    .ExecuteDelete();
            }
            return Results.NoContent();
        });

        return group;
    }
}