namespace Engage.Persistence.Configurations;

public class ProjectUserConfiguration : IEntityTypeConfiguration<ProjectUser>
{
    public void Configure(EntityTypeBuilder<ProjectUser> builder)
    {
        builder.HasKey(e => new { e.ProjectId, e.UserId })
            .IsClustered(false);

        builder.HasOne(x => x.Project)
            .WithMany(c => c.ProjectUsers)
            .HasForeignKey(x => x.ProjectId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(x => x.User)
            .WithMany(c => c.ProjectUsers)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}