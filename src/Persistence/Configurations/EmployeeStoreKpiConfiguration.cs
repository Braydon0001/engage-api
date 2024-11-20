namespace Engage.Persistence.Configurations;

public class EmployeeStoreKpiConfiguration : IEntityTypeConfiguration<EmployeeStoreKpi>
{
    public void Configure(EntityTypeBuilder<EmployeeStoreKpi> builder)
    {
        builder.HasKey(e => new { e.EmployeeId, e.StoreId, e.EmployeeKpiId })
            .IsClustered(false);

        builder.HasOne(x => x.Employee)
            .WithMany(c => c.EmployeeStoreKpis)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(x => x.Store)
            .WithMany(c => c.EmployeeStoreKpis)
            .HasForeignKey(x => x.StoreId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(x => x.EmployeeKpi)
            .WithMany(c => c.EmployeeStoreKpis)
            .HasForeignKey(x => x.EmployeeKpiId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
