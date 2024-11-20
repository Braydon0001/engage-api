// auto-generated
namespace Engage.Persistence.Configurations;

public class StoreOwnerConfiguration : IEntityTypeConfiguration<StoreOwner>
{
    public void Configure(EntityTypeBuilder<StoreOwner> builder)
    {
        builder.Property(e => e.StoreOwnerId).IsRequired();
        builder.Property(e => e.StoreId).IsRequired();
        builder.Property(e => e.StoreGroupId).IsRequired();
        builder.Property(e => e.StoreOwnerTypeId).IsRequired();
        builder.Property(e => e.StartDate).IsRequired();
        builder.Property(e => e.EndDate);
        builder.Property(e => e.Note).HasMaxLength(1000);
        builder.Property(e => e.Name).HasMaxLength(1000);
    }
}