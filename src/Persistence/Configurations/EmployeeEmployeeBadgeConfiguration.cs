namespace Engage.Persistence.Configurations;

public class EmployeeEmployeeBadgeConfiguration : IEntityTypeConfiguration<EmployeeEmployeeBadge>
{
    public void Configure(EntityTypeBuilder<EmployeeEmployeeBadge> builder)
    {
        builder.HasKey(e => new { e.EmployeeId, e.EmployeeBadgeId })
            .IsClustered(false);

        builder.HasOne(x => x.Employee)
            .WithMany(c => c.EmployeeBadges)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(x => x.EmployeeBadge)
            .WithMany(c => c.EmployeeBadges)
            .HasForeignKey(x => x.EmployeeBadgeId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
