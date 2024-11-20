namespace Engage.Persistence.Configurations;

public class StoreConfiguration : IEntityTypeConfiguration<Store>
{
    public void Configure(EntityTypeBuilder<Store> builder)
    {
        builder.HasIndex(e => new { e.StoreId, e.ParentStoreId })
               .IsUnique()
               .IsClustered(false);

        builder.Property(e => e.Code)
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(120);

        builder.Property(e => e.CatManStoreCode)
            .HasMaxLength(30);

        builder.Property(e => e.StoreImageUrl)
            .HasMaxLength(300);

        builder.HasOne(x => x.Stakeholder)
            .WithOne(s => s.Store)
            .HasForeignKey<Store>(k => k.StakeholderId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.Property(e => e.VatNumber)
            .HasMaxLength(30);
    }
}
