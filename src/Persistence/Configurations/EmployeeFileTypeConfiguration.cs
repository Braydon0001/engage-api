namespace Engage.Persistence.Configurations;

public class EmployeeFileTypeConfiguration : IEntityTypeConfiguration<EmployeeFileType>
{
    public void Configure(EntityTypeBuilder<EmployeeFileType> builder)
    {
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(120);

        builder.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(300);
    }
}
