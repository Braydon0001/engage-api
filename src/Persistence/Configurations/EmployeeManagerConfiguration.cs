namespace Engage.Persistence.Configurations;

public class EmployeeManagerConfiguration : IEntityTypeConfiguration<EmployeeManager>
{
    public void Configure(EntityTypeBuilder<EmployeeManager> builder)
    {
        builder.HasKey(e => new { e.EmployeeId, e.ManagerId }).IsClustered(false);

        builder.HasOne(x => x.Employee)
            .WithMany(s => s.EmployeeManagers)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(x => x.Manager)
            .WithMany(s => s.ManagerEmployees)
            .HasForeignKey(x => x.ManagerId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
