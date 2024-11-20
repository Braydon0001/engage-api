namespace Engage.Persistence.Configurations;

public class VendorConfiguration : IEntityTypeConfiguration<Vendor>
{
    public void Configure(EntityTypeBuilder<Vendor> builder)
    {
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(120);

        builder.Property(e => e.AccountNumber)
            .HasMaxLength(30);

        builder.HasOne(x => x.DistributionCenter)
            .WithMany(e => e.Vendors)
            .HasForeignKey(x => x.DistributionCenterId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(x => x.Supplier)
            .WithMany(e => e.Vendors)
            .HasForeignKey(x => x.SupplierId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
