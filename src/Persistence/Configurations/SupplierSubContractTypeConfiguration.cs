// auto-generated
namespace Engage.Persistence.Configurations;

public class SupplierSubContractTypeConfiguration : IEntityTypeConfiguration<SupplierSubContractType>
{
    public void Configure(EntityTypeBuilder<SupplierSubContractType> builder)
    {
        builder.Property(e => e.SupplierSubContractTypeId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}