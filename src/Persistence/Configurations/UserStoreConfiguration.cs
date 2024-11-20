namespace Engage.Persistence.Configurations;

public class UserStoreConfiguration : IEntityTypeConfiguration<UserStore>
{
    public void Configure(EntityTypeBuilder<UserStore> builder)
    {
        builder.Property(e => e.UserStoreId).IsRequired();
        builder.Property(e => e.UserId).IsRequired();
        builder.Property(e => e.StoreId).IsRequired();
    }
}