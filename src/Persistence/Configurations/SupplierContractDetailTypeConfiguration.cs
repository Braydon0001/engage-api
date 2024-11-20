// auto-generated
namespace Engage.Persistence.Configurations;

public class SupplierContractDetailTypeConfiguration : IEntityTypeConfiguration<SupplierContractDetailType>
{
    public void Configure(EntityTypeBuilder<SupplierContractDetailType> builder)
    {
        builder.Property(e => e.SupplierContractDetailTypeId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}