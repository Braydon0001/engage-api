namespace Engage.Persistence.Configurations;

public class EmployeeEmployeeKpiConfiguration : IEntityTypeConfiguration<EmployeeEmployeeKpi>
{
    public void Configure(EntityTypeBuilder<EmployeeEmployeeKpi> builder)
    {
        builder.HasKey(e => new { e.EmployeeId, e.EmployeeKpiId })
            .IsClustered(false);

        builder.HasOne(x => x.Employee)
            .WithMany(c => c.EmployeeKpis)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(x => x.EmployeeKpi)
            .WithMany(c => c.EmployeeKpis)
            .HasForeignKey(x => x.EmployeeKpiId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
