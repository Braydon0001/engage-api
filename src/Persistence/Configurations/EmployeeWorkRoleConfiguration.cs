namespace Engage.Persistence.Configurations;

public class EmployeeWorkRoleConfiguration : IEntityTypeConfiguration<EmployeeWorkRole>
{
    public void Configure(EntityTypeBuilder<EmployeeWorkRole> builder)
    {
        builder.Property(e => e.Title)
            .HasMaxLength(300);

        //builder.Property(e => e.Note)
        //    .HasColumnType("ntext");

        builder.Property(e => e.GradeLevel)
            .HasMaxLength(60);

        builder.HasOne(x => x.Employee)
            .WithMany(e => e.WorkRoles)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.ClientCascade);
    }
}
