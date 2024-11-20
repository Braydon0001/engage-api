// auto-generated
namespace Engage.Persistence.Configurations;

public class SupplierBudgetConfiguration : IEntityTypeConfiguration<SupplierBudget>
{
    public void Configure(EntityTypeBuilder<SupplierBudget> builder)
    {
        builder.Property(e => e.SupplierBudgetId).IsRequired();
        builder.Property(e => e.SupplierBudgetVersionId).IsRequired();
        builder.Property(e => e.SupplierBudgetTypeId).IsRequired();
        builder.Property(e => e.SupplierId).IsRequired();
        builder.Property(e => e.SupplierContractDetailId);
        builder.Property(e => e.EngageRegionId);
        builder.Property(e => e.Amount).IsRequired();
    }
}