// auto-generated
namespace Engage.Persistence.Configurations;

public class WebFileCategoryConfiguration : IEntityTypeConfiguration<WebFileCategory>
{
    public void Configure(EntityTypeBuilder<WebFileCategory> builder)
    {
        builder.Property(e => e.WebFileCategoryId);
        builder.Property(e => e.WebFileGroupId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(30);
        builder.Property(e => e.DisplayName).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Order).IsRequired();
    }
}