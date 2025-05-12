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
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Epic>(eb =>
        {
            eb.Property(wi => wi.EndDate).HasPrecision(3);
        });

        modelBuilder.Entity<Task>( eb =>
        {
            eb.Property(wi => wi.Activity).HasMaxLength(200);
            eb.Property(wi => wi.RemainingWork).HasPrecision(14, 2);
        });

        modelBuilder.Entity<Issue>(eb => eb.Property(wi => wi.Efford).HasColumnType("decimal(5,2)"));

        modelBuilder.Entity<WorkItem>(eb =>
        {
            eb.Property(wi => wi.Area).HasColumnType("varchar(200)");
            eb.Property(wi => wi.IterationPath).HasColumnName("Iteration_Path");
            eb.Property(wi => wi.Priority).HasDefaultValue(1);

            eb.HasMany(wi => wi.Comments)
                .WithOne(c => c.WorkItem)
                .HasForeignKey(c => c.WorkItemId);

            eb.HasOne(wi => wi.Author)
                .WithMany(u => u.WorkItems)
                .HasForeignKey(wi => wi.AuthorId);

            eb.HasMany(wi => wi.Tags)
                .WithMany(t => t.WorkItems)
                .UsingEntity<WorkItemTag>(
                    w => w.HasOne(wit => wit.Tag).WithMany().HasForeignKey(wit => wit.TagId),
                    w => w.HasOne(wit => wit.WorkItem).WithMany().HasForeignKey(wit => wit.WorkItemId),
                    wit =>
                    {
                        wit.HasKey(x => new { x.TagId, x.WorkItemId });
                        wit.Property(x => x.PublicationDate).HasDefaultValueSql("now()");
                    }
                );

            eb.HasOne(wi => wi.State)
                .WithMany()
                .HasForeignKey(wi => wi.StateId);
        });

        modelBuilder.Entity<Comment>(eb =>
        {
            eb.Property(x => x.CreatedTime).HasDefaultValueSql("now()");
            eb.Property(x => x.UpdatedTime).ValueGeneratedOnUpdate();
        });

        modelBuilder.Entity<User>()
            .HasOne(u => u.Address)
            .WithOne(a => a.User)
            .HasForeignKey<Address>(a => a.UserId);

        modelBuilder.Entity<WorkItemState>(eb =>
        {
            eb.Property(s => s.Value).IsRequired().HasMaxLength(60);
        });
    }
}