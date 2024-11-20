namespace Engage.Persistence.Configurations;

public class EmployeeJobTitleConfiguration : IEntityTypeConfiguration<EmployeeJobTitle>
{
    public void Configure(EntityTypeBuilder<EmployeeJobTitle> builder)
    {
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(120);

        builder.Property(e => e.Description)
            .HasMaxLength(120);
    }
}
