namespace Engage.Persistence.Configurations;

public class EmployeeFileConfiguration : IEntityTypeConfiguration<EmployeeFile>
{
    public void Configure(EntityTypeBuilder<EmployeeFile> builder)
    {
        builder.Property(e => e.Files).HasColumnType("json");
    }
}
