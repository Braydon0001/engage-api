namespace Engage.Persistence.Configurations;

public class CostCenterEmployeeConfiguration : IEntityTypeConfiguration<CostCenterEmployee>
{
    public void Configure(EntityTypeBuilder<CostCenterEmployee> builder)
    {
        builder.Property(e => e.CostCenterEmployeeId).IsRequired();
        builder.Property(e => e.CostCenterId).IsRequired();
        builder.Property(e => e.EmployeeId).IsRequired();
    }
}