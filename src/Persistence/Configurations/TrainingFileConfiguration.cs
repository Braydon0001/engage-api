namespace Engage.Persistence.Configurations;

public class TrainingFileConfiguration : IEntityTypeConfiguration<TrainingFile>
{
    public void Configure(EntityTypeBuilder<TrainingFile> builder)
    {
        builder.Property(e => e.Files).HasColumnType("json");
    }
}
