namespace Engage.Persistence.Configurations;

public class TrainingProviderConfiguration : IEntityTypeConfiguration<TrainingProvider>
{
    public void Configure(EntityTypeBuilder<TrainingProvider> builder)
    {
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(120);
    }
}
