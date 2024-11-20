namespace Engage.Persistence.Configurations;

public class EmployeeRegionContactConfiguration : IEntityTypeConfiguration<EmployeeRegionContact>
{
    public void Configure(EntityTypeBuilder<EmployeeRegionContact> builder)
    {
        builder.Property(e => e.MobilePhone)
            .HasMaxLength(30);
    }
}
