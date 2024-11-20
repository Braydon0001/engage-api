namespace Engage.Persistence.Configurations;

public class EmployeeDisciplinaryProcedureConfiguration : IEntityTypeConfiguration<EmployeeDisciplinaryProcedure>
{
    public void Configure(EntityTypeBuilder<EmployeeDisciplinaryProcedure> builder)
    {
        builder.Property(e => e.Description)
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(e => e.Files).HasColumnType("json");
    }
}
