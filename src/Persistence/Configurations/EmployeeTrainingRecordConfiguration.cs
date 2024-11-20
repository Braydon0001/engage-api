namespace Engage.Persistence.Configurations;

public class EmployeeTrainingRecordConfiguration : IEntityTypeConfiguration<EmployeeTrainingRecord>
{
    public void Configure(EntityTypeBuilder<EmployeeTrainingRecord> builder)
    {
        builder.Property(e => e.CourseName)
            .HasMaxLength(120);

        builder.Property(e => e.CertificateNo)
            .HasMaxLength(50);

        builder.Property(e => e.CourseResult)
            .HasMaxLength(50);

        builder.Property(e => e.InvoiceNo)
            .HasMaxLength(50);

        builder.Property(e => e.Facilitator)
            .HasMaxLength(120);

        builder.Property(e => e.Assessor)
            .HasMaxLength(120);

        builder.Property(e => e.Note)
            .HasMaxLength(300);

        builder.HasOne(x => x.Employee)
            .WithMany(e => e.EmployeeTrainingRecords)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.ClientCascade);
    }
}
