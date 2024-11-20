namespace Engage.Persistence.Configurations;

public class EmployeeTransactionRemunerationTypeConfiguration : IEntityTypeConfiguration<EmployeeTransactionRemunerationType>
{
    public void Configure(EntityTypeBuilder<EmployeeTransactionRemunerationType> builder)
    {
        builder.Property(e => e.EmployeeTransactionRemunerationTypeId).IsRequired();
        builder.Property(e => e.Name).HasMaxLength(120);
    }
}