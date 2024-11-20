namespace Engage.Persistence.Configurations;

public class EmployeeAddressConfiguration : IEntityTypeConfiguration<EmployeeAddress>
{
    public void Configure(EntityTypeBuilder<EmployeeAddress> builder)
    {
        builder.Property(e => e.UnitNumber)
            .HasMaxLength(15);

        builder.Property(e => e.ComplexName)
            .HasMaxLength(120);

        builder.Property(e => e.StreetNumber)
            .HasMaxLength(15);

        builder.Property(e => e.StreetName)
            .HasMaxLength(120);

        builder.Property(e => e.Suburb)
            .HasMaxLength(120);

        builder.Property(e => e.City)
            .HasMaxLength(120);

        builder.Property(e => e.Code)
            .HasMaxLength(15);

        builder.Property(e => e.PostalUnitNumber)
            .HasMaxLength(15);

        builder.Property(e => e.PostalComplexName)
            .HasMaxLength(120);

        builder.Property(e => e.PostalStreetNumber)
            .HasMaxLength(15);

        builder.Property(e => e.PostalStreetName)
            .HasMaxLength(120);

        builder.Property(e => e.PostalSuburb)
            .HasMaxLength(120);

        builder.Property(e => e.PostalCity)
            .HasMaxLength(120);

        builder.Property(e => e.PostalCode)
            .HasMaxLength(15);

        builder.Property(e => e.CareOfIntermediary)
            .HasMaxLength(120);
    }
}
