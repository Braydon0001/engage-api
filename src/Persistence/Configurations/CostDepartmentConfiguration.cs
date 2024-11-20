namespace Engage.Persistence.Configurations;

public class CostDepartmentConfiguration : IEntityTypeConfiguration<CostDepartment>
{
    public void Configure(EntityTypeBuilder<CostDepartment> builder)
    {
        builder.Property(e => e.CostDepartmentId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}