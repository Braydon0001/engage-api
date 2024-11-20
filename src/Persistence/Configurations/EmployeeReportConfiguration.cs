namespace Engage.Persistence.Configurations;

public class EmployeeReportConfiguration : IEntityTypeConfiguration<EmployeeReport>
{
    public void Configure(EntityTypeBuilder<EmployeeReport> builder)
    {
        builder.HasKey(e => new { e.EmployeeId, e.ReportId }).IsClustered(false);

        builder.HasOne(x => x.Report)
            .WithMany(d => d.EmployeeReports)
            .HasForeignKey(x => x.ReportId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(x => x.Employee)
            .WithMany(d => d.EmployeeReports)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
}
