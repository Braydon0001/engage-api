namespace Engage.Persistence.Configurations;

public class EmployeePopiConsentConfiguration : IEntityTypeConfiguration<EmployeePopiConsent>
{
    public void Configure(EntityTypeBuilder<EmployeePopiConsent> builder)
    {
        builder.Property(e => e.EmployeeId).IsRequired();
    }
}
