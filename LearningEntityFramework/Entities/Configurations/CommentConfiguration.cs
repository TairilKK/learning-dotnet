using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningEntityFramework.Entities.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.Property(x => x.CreatedDate).HasDefaultValueSql("now()");
        builder.Property(x => x.UpdatedDate).ValueGeneratedOnUpdate();
    }
}