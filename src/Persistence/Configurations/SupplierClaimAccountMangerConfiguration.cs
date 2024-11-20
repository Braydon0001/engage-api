namespace Engage.Persistence.Configurations
{
    public class SupplierClaimAccountMangerConfiguration : IEntityTypeConfiguration<SupplierClaimAccountManager>
    {
        public void Configure(EntityTypeBuilder<SupplierClaimAccountManager> builder)
        {
            builder.HasKey(e => new { e.SupplierId, e.UserId })
                   .IsClustered(false);

            builder.HasOne(x => x.Supplier)
                .WithMany(c => c.SupplierClaimAccountManagers)
                .HasForeignKey(x => x.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(x => x.User)
                .WithMany(c => c.SupplierClaimAccountManagers)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
