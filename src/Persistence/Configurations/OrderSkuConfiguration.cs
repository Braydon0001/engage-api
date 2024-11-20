namespace Engage.Persistence.Configurations;

public class OrderSkuConfiguration : IEntityTypeConfiguration<OrderSku>
{
    public void Configure(EntityTypeBuilder<OrderSku> builder)
    {
        builder.Property(e => e.Quantity).IsRequired();
        builder.Property(e => e.Code).HasMaxLength(30);
        builder.Property(e => e.Description).HasMaxLength(220);
        builder.Property(e => e.Suffix).HasMaxLength(100);
        builder.Property(e => e.Note).HasMaxLength(300);
        builder.Property(e => e.Files).HasColumnType("json");

        builder.HasOne(x => x.Order)
            .WithMany(e => e.OrderSkus)
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
