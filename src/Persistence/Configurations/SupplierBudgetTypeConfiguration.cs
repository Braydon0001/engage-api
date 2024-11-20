// auto-generated
namespace Engage.Persistence.Configurations;

public class SupplierBudgetTypeConfiguration : IEntityTypeConfiguration<SupplierBudgetType>
{
    public void Configure(EntityTypeBuilder<SupplierBudgetType> builder)
    {
        builder.Property(e => e.SupplierBudgetTypeId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}