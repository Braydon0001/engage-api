// auto-generated
namespace Engage.Persistence.Configurations;

public class SupplierContractSubGroupConfiguration : IEntityTypeConfiguration<SupplierContractSubGroup>
{
    public void Configure(EntityTypeBuilder<SupplierContractSubGroup> builder)
    {
        builder.Property(e => e.SupplierContractSubGroupId).IsRequired();
        builder.Property(e => e.SupplierContractGroupId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}