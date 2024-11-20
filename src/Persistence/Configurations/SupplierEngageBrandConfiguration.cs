namespace Engage.Persistence.Configurations;

public class SupplierEngageBrandConfiguration : IEntityTypeConfiguration<SupplierEngageBrand>
{
    public void Configure(EntityTypeBuilder<SupplierEngageBrand> builder)
    {
        builder.HasKey(e => new { e.SupplierId, e.EngageBrandId })
            .IsClustered(false);

        builder.HasOne(x => x.Supplier)
            .WithMany(e => e.SupplierEngageBrands)
            .HasForeignKey(x => x.SupplierId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(x => x.EngageBrand)
            .WithMany(e => e.SupplierEngageBrands)
            .HasForeignKey(x => x.EngageBrandId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
