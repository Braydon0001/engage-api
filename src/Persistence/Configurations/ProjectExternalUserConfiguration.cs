namespace Engage.Persistence.Configurations;

public class ProjectExternalUserConfiguration : IEntityTypeConfiguration<ProjectExternalUser>
{
    public void Configure(EntityTypeBuilder<ProjectExternalUser> builder)
    {
        builder.Property(e => e.ProjectExternalUserId).IsRequired();
        builder.Property(e => e.FirstName).IsRequired();
        builder.Property(e => e.LastName).IsRequired();
        builder.Property(e => e.Email);
        builder.Property(e => e.CellNumber);
    }
}