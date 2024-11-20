namespace Engage.Persistence.Configurations;

public class EmployeeRegionConfiguration : IEntityTypeConfiguration<EmployeeRegion>
{
    public void Configure(EntityTypeBuilder<EmployeeRegion> builder)
    {
        builder.HasKey(e => new { e.EmployeeId, e.EngageRegionId })
            .IsClustered(false);

        builder.HasOne(x => x.Employee)
            .WithMany(c => c.EmployeeRegions)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(x => x.EngageRegion)
            .WithMany(c => c.Employees)
            .HasForeignKey(x => x.EngageRegionId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
