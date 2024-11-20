namespace Engage.Persistence.Configurations;

public class EmployeeHealthConditionConfiguration : IEntityTypeConfiguration<EmployeeHealthCondition>
{
    public void Configure(EntityTypeBuilder<EmployeeHealthCondition> builder)
    {
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(120);

        builder.Property(e => e.Description)
            .HasMaxLength(120);
    }
}
