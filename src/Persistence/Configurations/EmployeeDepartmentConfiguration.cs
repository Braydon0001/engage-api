namespace Engage.Persistence.Configurations;

public class EmployeeDepartmentConfiguration : IEntityTypeConfiguration<EmployeeDepartment>
{
    public void Configure(EntityTypeBuilder<EmployeeDepartment> builder)
    {
        builder.HasKey(e => new { e.EmployeeId, e.EngageDepartmentId })
            .IsClustered(false);

        builder.HasOne(x => x.Employee)
            .WithMany(c => c.EmployeeDepartments)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(x => x.EngageDepartment)
            .WithMany(c => c.Employees)
            .HasForeignKey(x => x.EngageDepartmentId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
