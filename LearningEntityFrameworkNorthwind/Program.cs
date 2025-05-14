using LearningEntityFrameworkNorthwind.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<NorthwindDbContext>(options => options
    .UseSqlServer(builder.Configuration.GetConnectionString("NorthwindConnectionString"))
);
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("data", async (NorthwindDbContext db) =>
{
    var sampleData = await db.Products
        .AsNoTracking()
        .Take(100)
        .ToListAsync();

    return sampleData;
});

app.Run();
