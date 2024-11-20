namespace Engage.Persistence.Configurations;

public class EmployeeVehicleConfiguration : IEntityTypeConfiguration<EmployeeVehicle>
{
    public void Configure(EntityTypeBuilder<EmployeeVehicle> builder)
    {
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(120);

        builder.Property(e => e.Description)
            .HasMaxLength(100);

        builder.Property(e => e.Tracker)
            .HasMaxLength(100);

        builder.Property(e => e.Year)
            .HasMaxLength(100);

        builder.Property(e => e.RegistrationNumber)
            .HasMaxLength(100);

        builder.Property(e => e.Vin)
            .HasMaxLength(100);

        builder.Property(e => e.Note)
            .HasMaxLength(200);

    }
}

