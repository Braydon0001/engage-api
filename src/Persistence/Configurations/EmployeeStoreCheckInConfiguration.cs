namespace Engage.Persistence.Configurations;

public class EmployeeStoreCheckInConfiguration : IEntityTypeConfiguration<EmployeeStoreCheckIn>
{
    public void Configure(EntityTypeBuilder<EmployeeStoreCheckIn> builder)
    {
        builder.HasOne(x => x.Employee)
            .WithMany(s => s.EmployeeStoreCheckIns)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(x => x.Store)
            .WithMany(s => s.EmployeeStoreCheckIns)
            .HasForeignKey(x => x.StoreId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasIndex(x => x.CheckInUuid)
            .IsUnique();
    }
}
