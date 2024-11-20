namespace Engage.Persistence.Configurations;

public class EmployeeDeductionConfiguration : IEntityTypeConfiguration<EmployeeDeduction>
{
    public void Configure(EntityTypeBuilder<EmployeeDeduction> builder)
    {
        builder.Property(e => e.Amount)
            .IsRequired();

        //builder.Property(e => e.Note)
        //    .HasColumnType("ntext");

        builder.Property(e => e.Reference)
            .IsRequired()
            .HasMaxLength(120);

        builder.HasOne(x => x.Employee)
            .WithMany(s => s.Deductions)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
}
