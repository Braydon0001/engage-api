// auto-generated
namespace Engage.Persistence.Configurations;

public class SupplierBudgetVersionTypeConfiguration : IEntityTypeConfiguration<SupplierBudgetVersionType>
{
    public void Configure(EntityTypeBuilder<SupplierBudgetVersionType> builder)
    {
        builder.Property(e => e.SupplierBudgetVersionTypeId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}