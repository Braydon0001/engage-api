// auto-generated
namespace Engage.Persistence.Configurations;

public class WebFileGroupConfiguration : IEntityTypeConfiguration<WebFileGroup>
{
    public void Configure(EntityTypeBuilder<WebFileGroup> builder)
    {
        builder.Property(e => e.WebFileGroupId);
        builder.Property(e => e.Name).IsRequired().HasMaxLength(30);
    }
}