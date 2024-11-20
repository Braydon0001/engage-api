// auto-generated
namespace Engage.Persistence.Configurations;

public class SupplierContractGroupConfiguration : IEntityTypeConfiguration<SupplierContractGroup>
{
    public void Configure(EntityTypeBuilder<SupplierContractGroup> builder)
    {
        builder.Property(e => e.SupplierContractGroupId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}