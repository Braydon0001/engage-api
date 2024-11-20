namespace Engage.Persistence.Configurations;

public class SupplierSubContractDetailTypeConfiguration : IEntityTypeConfiguration<SupplierSubContractDetailType>
{
    public void Configure(EntityTypeBuilder<SupplierSubContractDetailType> builder)
    {
        builder.Property(e => e.SupplierSubContractDetailTypeId).IsRequired();
        builder.Property(e => e.Name).HasMaxLength(100);
    }
}
