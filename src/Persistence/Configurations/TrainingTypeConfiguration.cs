namespace Engage.Persistence.Configurations;

public class TrainingTypeConfiguration : IEntityTypeConfiguration<TrainingType>
{
    public void Configure(EntityTypeBuilder<TrainingType> builder)
    {
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(120);
    }
}
