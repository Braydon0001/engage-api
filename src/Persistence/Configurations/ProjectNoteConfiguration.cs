namespace Engage.Persistence.Configurations;

public class ProjectNoteConfiguration : IEntityTypeConfiguration<ProjectNote>
{
    public void Configure(EntityTypeBuilder<ProjectNote> builder)
    {
        builder.Property(e => e.ProjectNoteId).IsRequired();
        builder.Property(e => e.Note).IsRequired().HasMaxLength(1000);
        builder.Property(e => e.ProjectId);
        builder.Property(e => e.Files).HasColumnType("json");
    }
}