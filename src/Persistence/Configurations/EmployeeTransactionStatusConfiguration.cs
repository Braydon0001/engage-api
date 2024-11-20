// auto-generated
namespace Engage.Persistence.Configurations;

public class EmployeeTransactionStatusConfiguration : IEntityTypeConfiguration<EmployeeTransactionStatus>
{
    public void Configure(EntityTypeBuilder<EmployeeTransactionStatus> builder)
    {
        builder.Property(e => e.EmployeeTransactionStatusId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}