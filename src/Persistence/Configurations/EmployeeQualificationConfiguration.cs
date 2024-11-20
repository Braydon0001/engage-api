namespace Engage.Persistence.Configurations;

public class EmployeeQualificationConfiguration : IEntityTypeConfiguration<EmployeeQualification>
{
    public void Configure(EntityTypeBuilder<EmployeeQualification> builder)
    {
        builder.Property(e => e.Name)
            .HasMaxLength(120);

        builder.Property(e => e.InstitutionName)
            .HasMaxLength(120);

        builder.Property(e => e.Description)
            .HasMaxLength(120);

        builder.Property(e => e.FinalYearSubjects)
            .HasMaxLength(250);

        builder.HasOne(x => x.Employee)
            .WithMany(e => e.EmployeeQualifications)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.ClientCascade);

        builder.Property(e => e.Files).HasColumnType("json");
    }
}
