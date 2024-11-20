namespace Engage.Persistence.Configurations;

public class EmployeeFuelConfiguration : IEntityTypeConfiguration<EmployeeFuel>
{
    public void Configure(EntityTypeBuilder<EmployeeFuel> builder)
    {
        builder.Property(e => e.BlobUrl)
               .HasMaxLength(1000);

        builder.Property(e => e.BlobName)
                .HasMaxLength(1000);
    }
}
