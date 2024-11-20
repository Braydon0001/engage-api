namespace Engage.Persistence.Configurations;

public class EmployeeStoreArchiveConfiguration : IEntityTypeConfiguration<EmployeeStoreArchive>
{
    public void Configure(EntityTypeBuilder<EmployeeStoreArchive> builder)
    {
        builder.HasIndex(e => new { e.EmployeeId, e.StoreId, e.EngageSubGroupId })
               .IsUnique();

        builder.Property(e => e.Frequency)
               .IsRequired();

        builder.HasOne(x => x.Store)
               .WithMany(s => s.EmployeeStoreArchives)
               .HasForeignKey(x => x.StoreId)
               .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(x => x.Employee)
               .WithMany(s => s.EmployeeStoreArchives)
               .HasForeignKey(x => x.EmployeeId)
               .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
