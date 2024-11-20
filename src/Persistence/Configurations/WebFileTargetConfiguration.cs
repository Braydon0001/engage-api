// auto-generated
namespace Engage.Persistence.Configurations;

public class WebFileTargetConfiguration : IEntityTypeConfiguration<WebFileTarget>
{
    public void Configure(EntityTypeBuilder<WebFileTarget> builder)
    {
        builder.Property(e => e.WebFileTargetId).IsRequired();
        builder.Property(e => e.WebFileId).IsRequired();
    }
}