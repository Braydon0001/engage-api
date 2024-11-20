namespace Engage.Persistence.Configurations;

public class EmployeeLeaveEntryConfiguration : IEntityTypeConfiguration<EmployeeLeaveEntry>
{
    public void Configure(EntityTypeBuilder<EmployeeLeaveEntry> builder)
    {
        //builder.Property(e => e.Comment)
        //    .HasColumnType("ntext");

        //builder.Property(e => e.ManagerComment)
        //    .HasColumnType("ntext");
    }
}
