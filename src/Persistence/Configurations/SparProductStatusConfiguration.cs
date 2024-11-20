namespace Engage.Persistence.Configurations;

public class SparProductStatusConfiguration : IEntityTypeConfiguration<SparProductStatus>
{
    public void Configure(EntityTypeBuilder<SparProductStatus> builder)
    {
        builder.Property(e => e.SparProductStatusId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(120);
    }
}