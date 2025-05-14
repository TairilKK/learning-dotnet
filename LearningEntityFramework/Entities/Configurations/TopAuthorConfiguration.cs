using LearningEntityFramework.Entities.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningEntityFramework.Entities.Configurations;

public class TopAuthorConfiguration : IEntityTypeConfiguration<TopAuthor>
{
    public void Configure(EntityTypeBuilder<TopAuthor> builder)
    {
        builder.ToView("view_topauthors");
        builder.HasNoKey();
    }
}