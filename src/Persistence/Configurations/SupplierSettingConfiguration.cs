namespace Engage.Persistence.Configurations;

public class SupplierSettingConfiguration : IEntityTypeConfiguration<SupplierSetting>
{
    public void Configure(EntityTypeBuilder<SupplierSetting> builder)
    {
        builder.HasIndex(e => new { e.SettingId, e.SupplierId })
               .IsUnique()
               .IsClustered(false);

        builder.Property(e => e.Value)
               .HasMaxLength(200);


    }
}
