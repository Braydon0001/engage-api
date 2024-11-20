namespace Engage.Persistence.Configurations;

public class UserRegionConfiguration : IEntityTypeConfiguration<UserRegion>
{
    public void Configure(EntityTypeBuilder<UserRegion> builder)
    {
        builder.Property(e => e.UserRegionId).IsRequired();
        builder.Property(e => e.UserId).IsRequired();
        builder.Property(e => e.EngageRegionId).IsRequired();
    }
}