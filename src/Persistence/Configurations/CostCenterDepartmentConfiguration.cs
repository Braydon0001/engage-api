namespace Engage.Persistence.Configurations;

public class CostCenterDepartmentConfiguration : IEntityTypeConfiguration<CostCenterDepartment>
{
    public void Configure(EntityTypeBuilder<CostCenterDepartment> builder)
    {
        builder.Property(e => e.CostCenterDepartmentId).IsRequired();
        builder.Property(e => e.CostCenterId).IsRequired();
        builder.Property(e => e.CostDepartmentId).IsRequired();
    }
}