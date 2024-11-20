namespace Engage.Persistence.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.Property(e => e.Code)
            .IsRequired()
            .HasMaxLength(15);

        builder.Property(e => e.FirstName)
            .IsRequired()
            .HasMaxLength(120);

        builder.Property(e => e.LastName)
            .IsRequired()
            .HasMaxLength(120);

        builder.Property(e => e.MiddleName)
            .HasMaxLength(120);

        builder.Property(e => e.Initials)
            .HasMaxLength(10);

        builder.Property(e => e.KnownAs)
            .HasMaxLength(120);

        builder.Property(e => e.IdNumber)
            .HasMaxLength(30);

        builder.Property(e => e.PassportNumber)
            .HasMaxLength(30);

        builder.Property(e => e.PAYENumber)
            .HasMaxLength(15);

        builder.Property(e => e.SARSNumber)
            .HasMaxLength(15);

        builder.Property(e => e.UIFNumber)
            .HasMaxLength(15);

        builder.Property(e => e.RANumber)
            .HasMaxLength(15);

        builder.Property(e => e.MedicalAidNumber)
            .HasMaxLength(15);

        builder.Property(e => e.StartingDate)
            .IsRequired();

        builder.Property(e => e.NextOfKinName)
            .HasMaxLength(100);

        builder.Property(e => e.NextOfKinContactNumber)
            .HasMaxLength(15);

        builder.Property(e => e.EmailAddress1)
            .HasMaxLength(100);

        builder.Property(e => e.EmailAddress2)
           .HasMaxLength(100);

        builder.Property(e => e.Note)
           .HasMaxLength(300);

        builder.Property(e => e.BlobUrl)
              .HasMaxLength(1000);

        builder.Property(e => e.BlobName)
                .HasMaxLength(1000);

        builder.HasOne(x => x.Stakeholder)
               .WithOne(s => s.Employee)
               .HasForeignKey<Employee>(k => k.StakeholderId)
               .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(x => x.Manager)
               .WithMany(e => e.Employees)
               .HasForeignKey(x => x.ManagerId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .IsRequired(false);

        builder.HasOne(x => x.LeaveManager)
               .WithMany(e => e.LeaveEmployees)
               .HasForeignKey(x => x.LeaveManagerId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .IsRequired(false);

        builder.HasOne(x => x.CostCenterManager)
               .WithMany(e => e.CostCenterEmployees)
               .HasForeignKey(x => x.CostCenterManagerId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .IsRequired(false);

        builder.Property(e => e.Files).HasColumnType("json");
    }
}
