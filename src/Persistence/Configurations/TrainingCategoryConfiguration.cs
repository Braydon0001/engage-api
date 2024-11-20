namespace Engage.Persistence.Configurations;

public class TrainingCategoryConfiguration : IEntityTypeConfiguration<TrainingCategory>
{
    public void Configure(EntityTypeBuilder<TrainingCategory> builder)
    {
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(120);
    }
}
