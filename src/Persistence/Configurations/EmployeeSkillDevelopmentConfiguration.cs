namespace Engage.Persistence.Configurations;

public class EmployeeSkillDevelopmentConfiguration : IEntityTypeConfiguration<EmployeeSkillsDevelopment>
{
    public void Configure(EntityTypeBuilder<EmployeeSkillsDevelopment> builder)
    {
        builder.Property(e => e.Name)
            .HasMaxLength(120)
            .IsRequired();

        //builder.Property(e => e.Description)
        //    .HasColumnType("ntext");

    }
}
