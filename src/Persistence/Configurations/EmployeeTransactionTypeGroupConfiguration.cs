namespace Engage.Persistence.Configurations;

public class EmployeeTransactionTypeGroupConfiguration : IEntityTypeConfiguration<EmployeeTransactionTypeGroup>
{
    public void Configure(EntityTypeBuilder<EmployeeTransactionTypeGroup> builder)
    {
        builder.Property(e => e.EmployeeTransactionTypeGroupId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}