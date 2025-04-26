using Microsoft.EntityFrameworkCore;

namespace LearningMinimalAPI.Data;

public static class DataExtensions
{
    public static async Task MigrateDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
        dbContext.Database.MigrateAsync();
    }
}