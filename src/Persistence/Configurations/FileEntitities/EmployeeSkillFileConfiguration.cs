namespace Engage.Persistence.Configurations.FileEntitities;

public class EmployeeSkillFileConfiguration : IEntityTypeConfiguration<EmployeeSkillFile>
{
    public void Configure(EntityTypeBuilder<EmployeeSkillFile> builder)
    {
        builder.Property(e => e.Name)
            .HasMaxLength(1024)
            .IsRequired();

        builder.Property(e => e.Url)
            .HasMaxLength(1024)
            .IsRequired();

        builder.Property(e => e.Metadata)
            .HasMaxLength(4000);

    }
}
