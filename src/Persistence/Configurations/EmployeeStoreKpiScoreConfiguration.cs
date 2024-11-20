namespace Engage.Persistence.Configurations;

public class EmployeeStoreKpiScoreConfiguration : IEntityTypeConfiguration<EmployeeStoreKpiScore>
{
    public void Configure(EntityTypeBuilder<EmployeeStoreKpiScore> builder)
    {
        builder.HasOne(x => x.Employee)
            .WithMany(c => c.EmployeeStoreKpiScores)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(x => x.Store)
            .WithMany(c => c.EmployeeStoreKpiScores)
            .HasForeignKey(x => x.StoreId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(x => x.EmployeeKpi)
            .WithMany(c => c.EmployeeStoreKpiScores)
            .HasForeignKey(x => x.EmployeeKpiId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
