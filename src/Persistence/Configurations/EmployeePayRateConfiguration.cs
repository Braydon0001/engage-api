namespace Engage.Persistence.Configurations;

public class EmployeePayRateConfiguration : IEntityTypeConfiguration<EmployeePayRate>
{
    public void Configure(EntityTypeBuilder<EmployeePayRate> builder)
    {
        builder.Property(e => e.EffectiveDate)
            .IsRequired();

        builder.Property(e => e.IncreaseReason)
            .HasMaxLength(220);

        builder.Property(e => e.Note)
            .HasMaxLength(300);
    }
}
