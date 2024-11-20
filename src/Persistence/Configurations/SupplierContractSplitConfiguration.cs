// auto-generated
namespace Engage.Persistence.Configurations;

public class SupplierContractSplitConfiguration : IEntityTypeConfiguration<SupplierContractSplit>
{
    public void Configure(EntityTypeBuilder<SupplierContractSplit> builder)
    {
        builder.Property(e => e.SupplierContractSplitId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}