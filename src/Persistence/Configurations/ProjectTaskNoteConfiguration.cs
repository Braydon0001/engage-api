namespace Engage.Persistence.Configurations;

public class ProjectTaskNoteConfiguration : IEntityTypeConfiguration<ProjectTaskNote>
{
    public void Configure(EntityTypeBuilder<ProjectTaskNote> builder)
    {
        builder.Property(e => e.ProjectTaskNoteId).IsRequired();
        builder.Property(e => e.Note).IsRequired().HasMaxLength(1000);
        builder.Property(e => e.ProjectTaskId);
        builder.Property(e => e.Files).HasColumnType("json");
    }
}