namespace Engage.Persistence.Configurations;

public class EmployeeSkillConfiguration : IEntityTypeConfiguration<EmployeeSkill>
{
    public void Configure(EntityTypeBuilder<EmployeeSkill> builder)
    {
        builder.Property(e => e.Name)
            .HasMaxLength(120);

        builder.Property(e => e.Description)
            .HasMaxLength(120);

        builder.HasOne(x => x.Employee)
            .WithMany(e => e.EmployeeSkills)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.ClientCascade);

        builder.Property(e => e.Files).HasColumnType("json");
    }
}
