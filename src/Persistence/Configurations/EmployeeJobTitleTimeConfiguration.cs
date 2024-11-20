namespace Engage.Persistence.Configurations;

public class EmployeeJobTitleTimeConfiguration : IEntityTypeConfiguration<EmployeeJobTitleTime>
{
    public void Configure(EntityTypeBuilder<EmployeeJobTitleTime> builder)
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
