namespace Engage.Persistence.Configurations;

public class UserUserGroupConfiguration : IEntityTypeConfiguration<UserUserGroup>
{
    public void Configure(EntityTypeBuilder<UserUserGroup> builder)
    {
        builder.HasIndex(e => new { e.UserId, e.UserGroupId })
               .IsUnique();

        builder.HasOne(x => x.User)
            .WithMany(c => c.UserGroups)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(x => x.UserGroup)
            .WithMany(c => c.Users)
            .HasForeignKey(x => x.UserGroupId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
