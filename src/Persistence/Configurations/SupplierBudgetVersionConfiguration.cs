// auto-generated
namespace Engage.Persistence.Configurations;

public class SupplierBudgetVersionConfiguration : IEntityTypeConfiguration<SupplierBudgetVersion>
{
    public void Configure(EntityTypeBuilder<SupplierBudgetVersion> builder)
    {
        builder.Property(e => e.SupplierBudgetVersionId).IsRequired();
        builder.Property(e => e.SupplierPeriodId).IsRequired();
        builder.Property(e => e.SupplierBudgetVersionTypeId).IsRequired();
    }
}