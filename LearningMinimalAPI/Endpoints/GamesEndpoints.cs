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
        group.MapGet("/", (GameStoreContext dbContext) =>
            dbContext.Games
                .Include(game=>game.Genre)
                .Select(game => game.ToGameSummaryDto())
                .AsNoTracking()
        );

        //GET: /games/id
        group.MapGet("/{id:int}", (int id, GameStoreContext dbContext) =>
        {
            Game? game = dbContext.Games.Find(id);

            return game is null ?
                Results.NotFound() : Results.Ok(game.ToGameDetailsDto());

        }).WithName(GetGameEndpointName);

        //POST: /games
        group.MapPost("/", (CreateGameDto newGame, GameStoreContext dbContext) =>
        {
            var game = newGame.ToEntity();
            game.Genre = dbContext.Genres.Find(newGame.GenreId);

            dbContext.Games.Add(game);
            dbContext.SaveChanges();

            return Results.CreatedAtRoute(
                GetGameEndpointName,
                new { id = game.Id },
                game.ToGameSummaryDto());
        });

        // PUT: /games/id
        group.MapPut("/{id:int}", (int id, UpdateGameDto updatedGame, GameStoreContext dbContext) =>
        {
            var existingGame = dbContext.Games.Find(id);
            if (existingGame is null)
            {
                return Results.NotFound();
            }

            dbContext.Entry(existingGame)
                .CurrentValues
                .SetValues(updatedGame.ToEntity(id));
            dbContext.SaveChanges();

            return Results.NoContent();
        });

        // DELETE: /games/id
        group.MapDelete("/{id:int}", (int id, GameStoreContext dbContext) =>
        {
            var existingGame = dbContext.Games.Find(id);
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