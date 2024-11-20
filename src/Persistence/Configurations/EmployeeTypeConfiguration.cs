namespace Engage.Persistence.Configurations;

public class EmployeeTypeConfiguration : IEntityTypeConfiguration<EmployeeType>
{
    public void Configure(EntityTypeBuilder<EmployeeType> builder)
    {
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(120);

        builder.Property(e => e.Description)
            .HasMaxLength(300);
    }
}
