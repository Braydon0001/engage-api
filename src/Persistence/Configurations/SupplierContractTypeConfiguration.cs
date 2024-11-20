// auto-generated
namespace Engage.Persistence.Configurations;

public class SupplierContractTypeConfiguration : IEntityTypeConfiguration<SupplierContractType>
{
    public void Configure(EntityTypeBuilder<SupplierContractType> builder)
    {
        builder.Property(e => e.SupplierContractTypeId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}