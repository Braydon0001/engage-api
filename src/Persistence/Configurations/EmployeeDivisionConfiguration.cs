namespace Engage.Persistence.Configurations;

public class EmployeeDivisionConfiguration : IEntityTypeConfiguration<EmployeeDivision>
{
    public void Configure(EntityTypeBuilder<EmployeeDivision> builder)
    {
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(120);

        builder.Property(e => e.Description)
            .HasMaxLength(120);
    }
}
