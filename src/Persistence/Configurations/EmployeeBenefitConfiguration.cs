namespace Engage.Persistence.Configurations;

public class EmployeeBenefitConfiguration : IEntityTypeConfiguration<EmployeeBenefit>
{
    public void Configure(EntityTypeBuilder<EmployeeBenefit> builder)
    {
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(120);

        //builder.Property(e => e.Description)
        //    .HasColumnType("ntext");

        builder.Property(e => e.Value)
            .IsRequired();

        builder.HasOne(x => x.Employee)
            .WithMany(s => s.Benefits)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
