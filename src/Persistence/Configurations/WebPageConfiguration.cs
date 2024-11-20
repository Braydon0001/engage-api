// auto-generated
namespace Engage.Persistence.Configurations;

public class WebPageConfiguration : IEntityTypeConfiguration<WebPage>
{
    public void Configure(EntityTypeBuilder<WebPage> builder)
    {
        builder.Property(e => e.WebPageId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}