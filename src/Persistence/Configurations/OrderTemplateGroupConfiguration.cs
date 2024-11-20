namespace Engage.Persistence.Configurations;

public class OrderTemplateGroupConfiguration : IEntityTypeConfiguration<OrderTemplateGroup>
{
    public void Configure(EntityTypeBuilder<OrderTemplateGroup> builder)
    {
        builder.Property(e => e.OrderTemplateGroupId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Description).HasMaxLength(100);
        builder.Property(e => e.Order).IsRequired();
    }
}