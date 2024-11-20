namespace Engage.Persistence.Configurations;

public class EmployeeSuspensionConfiguration : IEntityTypeConfiguration<EmployeeSuspension>
{
    public void Configure(EntityTypeBuilder<EmployeeSuspension> builder)
    {
        builder.HasOne(x => x.Employee)
            .WithMany(e => e.EmployeeSuspensions)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.ClientCascade);

        builder.Property(e => e.Files).HasColumnType("json");
    }
}
