namespace Engage.Persistence.Configurations;

public class TrainingFileTypeConfiguration : IEntityTypeConfiguration<TrainingFileType>
{
    public void Configure(EntityTypeBuilder<TrainingFileType> builder)
    {
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(120);

        builder.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(300);
    }
}
