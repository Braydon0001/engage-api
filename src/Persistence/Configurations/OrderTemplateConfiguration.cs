namespace Engage.Persistence.Configurations;

public class OrderTemplateConfiguration : IEntityTypeConfiguration<OrderTemplate>
{
    public void Configure(EntityTypeBuilder<OrderTemplate> builder)
    {
        builder.Property(e => e.OrderTemplateId).IsRequired();
        builder.Property(e => e.OrderTemplateStatusId).IsRequired();
        builder.Property(e => e.DistributionCenterId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.StartDate).IsRequired();
        builder.Property(e => e.EndDate);
        builder.Property(e => e.Note).HasMaxLength(1000);
        builder.Property(e => e.Files).HasColumnType("json");
    }
}