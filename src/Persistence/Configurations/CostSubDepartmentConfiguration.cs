namespace Engage.Persistence.Configurations;

public class CostSubDepartmentConfiguration : IEntityTypeConfiguration<CostSubDepartment>
{
    public void Configure(EntityTypeBuilder<CostSubDepartment> builder)
    {
        builder.Property(e => e.CostSubDepartmentId).IsRequired();
        builder.Property(e => e.CostDepartmentId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}