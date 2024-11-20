// auto-generated
namespace Engage.Persistence.Configurations;

public class SupplierContractAmountTypeConfiguration : IEntityTypeConfiguration<SupplierContractAmountType>
{
    public void Configure(EntityTypeBuilder<SupplierContractAmountType> builder)
    {
        builder.Property(e => e.SupplierContractAmountTypeId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}