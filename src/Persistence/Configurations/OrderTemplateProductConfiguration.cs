namespace Engage.Persistence.Configurations;

public class OrderTemplateProductConfiguration : IEntityTypeConfiguration<OrderTemplateProduct>
{
    public void Configure(EntityTypeBuilder<OrderTemplateProduct> builder)
    {
        builder.Property(e => e.OrderTemplateProductId).IsRequired();
        builder.Property(e => e.OrderTemplateGroupId).IsRequired();
        builder.Property(e => e.DCProductId).IsRequired();
        builder.Property(e => e.Order).IsRequired();
        builder.Property(e => e.Quantity).IsRequired();
        builder.Property(e => e.Price).IsRequired();
        builder.Property(e => e.PromotionPrice).IsRequired();
        builder.Property(e => e.RecommendedPrice).IsRequired();
        builder.Property(e => e.GrossProfitPercent).IsRequired();
        builder.Property(e => e.Suffix).HasMaxLength(100);
        builder.Property(e => e.Note).HasMaxLength(1000);
        builder.Property(e => e.Files).HasColumnType("json");
    }
}