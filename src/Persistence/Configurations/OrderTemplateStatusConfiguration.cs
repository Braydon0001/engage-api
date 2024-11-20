// auto-generated
namespace Engage.Persistence.Configurations;

public class OrderTemplateStatusConfiguration : IEntityTypeConfiguration<OrderTemplateStatus>
{
    public void Configure(EntityTypeBuilder<OrderTemplateStatus> builder)
    {
        builder.Property(e => e.OrderTemplateStatusId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}