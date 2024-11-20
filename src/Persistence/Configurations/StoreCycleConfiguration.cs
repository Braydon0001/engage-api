namespace Engage.Persistence.Configurations;

public class StoreCycleConfiguration : IEntityTypeConfiguration<StoreCycle>
{
    public void Configure(EntityTypeBuilder<StoreCycle> builder)
    {
        builder.Property(e => e.StoreCycleId).IsRequired();
        builder.Property(e => e.StoreId).IsRequired();
        builder.Property(e => e.EngageDepartmentId).IsRequired();
        builder.Property(e => e.StoreCycleOperationId).IsRequired();
        builder.Property(e => e.FrequencyTypeId).IsRequired();
        builder.Property(e => e.Note).HasMaxLength(1000);
    }
}