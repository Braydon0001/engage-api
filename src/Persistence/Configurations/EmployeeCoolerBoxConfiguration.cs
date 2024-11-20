namespace Engage.Persistence.Configurations;

public class EmployeeCoolerBoxConfiguration : IEntityTypeConfiguration<EmployeeCoolerBox>
{
    public void Configure(EntityTypeBuilder<EmployeeCoolerBox> builder)
    {
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(120);

        builder.Property(e => e.Description)
            .HasMaxLength(100);

        builder.Property(e => e.Note)
            .HasMaxLength(200);

        builder.Property(e => e.Files).HasColumnType("json");
    }
}

