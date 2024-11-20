namespace Engage.Persistence.Configurations;

public class TrainingDurationConfiguration : IEntityTypeConfiguration<TrainingDuration>
{
    public void Configure(EntityTypeBuilder<TrainingDuration> builder)
    {
        builder.Property(e => e.TrainingDurationId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(200);
    }
}
