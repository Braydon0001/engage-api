namespace Engage.Persistence.Configurations;

public class EmployeeEmployeeDivisionConfiguration : IEntityTypeConfiguration<EmployeeEmployeeDivision>
{
    public void Configure(EntityTypeBuilder<EmployeeEmployeeDivision> builder)
    {
        builder.HasKey(e => new { e.EmployeeId, e.EmployeeDivisionId })
            .IsClustered(false);

        builder.HasOne(x => x.Employee)
            .WithMany(c => c.EmployeeDivisions)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(x => x.EmployeeDivision)
            .WithMany(c => c.EmployeeEmployeeDivisions)
            .HasForeignKey(x => x.EmployeeDivisionId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
