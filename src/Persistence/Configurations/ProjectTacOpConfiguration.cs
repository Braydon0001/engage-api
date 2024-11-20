namespace Engage.Persistence.Configurations;

public class ProjectTacOpConfiguration : IEntityTypeConfiguration<ProjectTacOp>
{
    public void Configure(EntityTypeBuilder<ProjectTacOp> builder)
    {
        builder.Property(e => e.ProjectTacOpId).IsRequired();
        builder.Property(e => e.UserId).IsRequired();
        builder.Property(e => e.MobilePhone).HasMaxLength(20);
    }
}