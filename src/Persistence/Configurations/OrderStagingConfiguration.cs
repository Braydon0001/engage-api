namespace Engage.Persistence.Configurations;

public class OrderStagingConfiguration : IEntityTypeConfiguration<OrderStaging>
{
    public void Configure(EntityTypeBuilder<OrderStaging> builder)
    {
        builder.Property(e => e.OrderStagingId).IsRequired();
        builder.Property(e => e.Region).HasMaxLength(120);
        builder.Property(e => e.StoreName).HasMaxLength(120);
        builder.Property(e => e.AccountNumber).HasMaxLength(120);
        builder.Property(e => e.OrderNumber).HasMaxLength(120);
        builder.Property(e => e.OrderContactName).HasMaxLength(120);
        builder.Property(e => e.OrderContactEmail).HasMaxLength(120);
        builder.Property(e => e.VatNumber).HasMaxLength(120);
        builder.Property(e => e.Date).HasMaxLength(60);
        builder.Property(e => e.Reference).HasMaxLength(120);
    }
}