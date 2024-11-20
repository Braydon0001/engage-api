namespace Engage.Persistence.Configurations;

public class EmployeeAssetConfiguration : IEntityTypeConfiguration<EmployeeAsset>
{
    public void Configure(EntityTypeBuilder<EmployeeAsset> builder)
    {
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(120);

        builder.Property(e => e.Description)
            .HasMaxLength(100);

        builder.Property(e => e.Contract)
            .HasMaxLength(100);

        builder.Property(e => e.MobileNumber)
            .HasMaxLength(100);

        builder.Property(e => e.Sim)
            .HasMaxLength(100);

        builder.Property(e => e.Imei)
            .HasMaxLength(100);

        builder.Property(e => e.SerialNumber)
            .HasMaxLength(100);

        builder.Property(e => e.Note)
            .HasMaxLength(200);

        builder.Property(e => e.Files).HasColumnType("json");

    }
}

