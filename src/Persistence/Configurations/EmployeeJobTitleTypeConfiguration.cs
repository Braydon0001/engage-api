namespace Engage.Persistence.Configurations;

public class EmployeeJobTitleTypeConfiguration : IEntityTypeConfiguration<EmployeeJobTitleType>
{
    public void Configure(EntityTypeBuilder<EmployeeJobTitleType> builder)
    {
        builder.Property(e => e.Name)
               .IsRequired()
               .HasMaxLength(120);

        builder.Property(e => e.EmployeeJobTitleId)
               .IsRequired();

        builder.Property(e => e.Description)
               .HasMaxLength(300);
    }
}
