namespace Engage.Persistence.Configurations;

public class SubContractorBrandConfiguration : IEntityTypeConfiguration<SubContractorBrand>
{
    public void Configure(EntityTypeBuilder<SubContractorBrand> builder)
    {
        builder.Property(e => e.SubContractorBrandId).IsRequired();
        builder.Property(e => e.SupplierId).IsRequired();
        builder.Property(e => e.EngageBrandId).IsRequired();
        builder.Property(e => e.EngageRegionId).IsRequired();
        builder.Property(e => e.ParentId);

    }
}