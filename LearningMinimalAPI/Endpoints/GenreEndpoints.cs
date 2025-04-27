using LearningMinimalAPI.Data;
using LearningMinimalAPI.Mapping;
using Microsoft.EntityFrameworkCore;

namespace LearningMinimalAPI.Endpoints;

public static class GenreEndpoints
{
    public static RouteGroupBuilder MapGenresEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("genres");

        // GET: /genres
        group.MapGet("/", async (GameStoreContext dbContext) =>
            await dbContext.Genres
                .Select(genre => genre.ToDto())
                .AsNoTracking()
                .ToListAsync()
        );

        return group;
    }
}