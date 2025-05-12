using Microsoft.EntityFrameworkCore;

namespace LearningEntityFramework.Entities;

public class MyBoardsContext(DbContextOptions<MyBoardsContext> options): DbContext(options)
{
    public DbSet<WorkItem> WorkItems { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Address> Addresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WorkItem>()
            .Property(x => x.State)
            .IsRequired();

        modelBuilder.Entity<WorkItem>(eb =>
        {
            eb.Property(wi => wi.Area).HasColumnType("varchar(200)");
            eb.Property(wi => wi.IterationPath).HasColumnName("Iteration_Path");
            eb.Property(wi => wi.EndDate).HasPrecision(3);
            eb.Property(wi => wi.Efford).HasColumnType("decimal(5,2)");
            eb.Property(wi => wi.Activity).HasMaxLength(200);
            eb.Property(wi => wi.RemainingWork).HasPrecision(14, 2);
        });
    }
}