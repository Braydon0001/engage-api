namespace Engage.Persistence.Configurations;

public class EmployeeStoreConfiguration : IEntityTypeConfiguration<EmployeeStore>
{
    public void Configure(EntityTypeBuilder<EmployeeStore> builder)
    {
        builder.HasIndex(e => new { e.EmployeeId, e.StoreId, e.EngageSubGroupId })
               .IsUnique();

        builder.Property(e => e.Frequency)
               .IsRequired();

        builder.HasOne(x => x.Store)
               .WithMany(s => s.EmployeeStores)
               .HasForeignKey(x => x.StoreId)
               .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(x => x.Employee)
               .WithMany(s => s.EmployeeStores)
               .HasForeignKey(x => x.EmployeeId)
               .OnDelete(DeleteBehavior.ClientSetNull);
    }
}   
