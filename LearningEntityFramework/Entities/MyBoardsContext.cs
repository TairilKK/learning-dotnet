using LearningEntityFramework.Entities.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace LearningEntityFramework.Entities;

public class MyBoardsContext(DbContextOptions<MyBoardsContext> options): DbContext(options)
{
    public DbSet<WorkItem> WorkItems { get; set; }
    public DbSet<Issue> Issues { get; set; }
    public DbSet<Epic> Epics { get; set; }
    public DbSet<Task> Tasks { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<WorkItemState> WorkItemStates { get; set; }
    public DbSet<TopAuthor> ViewTopAuthors { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
}