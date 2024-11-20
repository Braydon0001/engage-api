namespace Engage.Persistence.Configurations;

public class EmployeeEmployeeJobTitleConfiguration : IEntityTypeConfiguration<EmployeeEmployeeJobTitle>
{
    public void Configure(EntityTypeBuilder<EmployeeEmployeeJobTitle> builder)
    {
        builder.HasKey(e => new { e.EmployeeId, e.EmployeeJobTitleId })
            .IsClustered(false);

        builder.HasOne(x => x.Employee)
            .WithMany(c => c.EmployeeJobTitles)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(x => x.EmployeeJobTitle)
            .WithMany(c => c.EmployeeEmployeeJobTitles)
            .HasForeignKey(x => x.EmployeeJobTitleId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
