using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningEntityFramework.Entities.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
   {
       builder.HasMany(u => u.Comments)
           .WithOne(c => c.Author)
           .HasForeignKey(c => c.AuthorId)
           .OnDelete(DeleteBehavior.ClientCascade);

       builder.HasOne(u => u.Address)
           .WithOne(a => a.User)
           .HasForeignKey<Address>(a => a.UserId);
    }
}