namespace Engage.Persistence.Configurations;

public class EmployeeKpiScoreConfiguration : IEntityTypeConfiguration<EmployeeKpiScore>
{
    public void Configure(EntityTypeBuilder<EmployeeKpiScore> builder)
    {
        builder.HasOne(x => x.Employee)
            .WithMany(c => c.EmployeeKpiScores)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(x => x.EmployeeKpi)
            .WithMany(c => c.EmployeeKpiScores)
            .HasForeignKey(x => x.EmployeeKpiId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
