namespace Engage.Persistence.Configurations;

public class SupplierStoreConfiguration : IEntityTypeConfiguration<SupplierStore>
{
    public void Configure(EntityTypeBuilder<SupplierStore> builder)
    {
        builder.HasIndex(e => new { e.SupplierId, e.StoreId, e.EngageSubGroupId })
            .IsUnique();

        builder.Property(e => e.AccountNumber)
           .HasMaxLength(120);

        builder.Property(e => e.Frequency)
            .IsRequired();

        builder.HasOne(x => x.Store)
            .WithMany(s => s.SupplierStores)
            .HasForeignKey(x => x.StoreId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(x => x.Supplier)
            .WithMany(s => s.SupplierStores)
            .HasForeignKey(x => x.SupplierId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
