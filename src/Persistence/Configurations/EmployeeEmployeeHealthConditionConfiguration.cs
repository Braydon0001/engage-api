namespace Engage.Persistence.Configurations;

public class EmployeeEmployeeHealthConditionConfiguration : IEntityTypeConfiguration<EmployeeEmployeeHealthCondition>
{
    public void Configure(EntityTypeBuilder<EmployeeEmployeeHealthCondition> builder)
    {
        builder.HasKey(e => new { e.EmployeeId, e.EmployeeHealthConditionId })
            .IsClustered(false);

        builder.HasOne(x => x.Employee)
            .WithMany(c => c.EmployeeHealthConditions)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(x => x.EmployeeHealthCondition)
            .WithMany(c => c.EmployeeEmployeeHealthConditions)
            .HasForeignKey(x => x.EmployeeHealthConditionId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
