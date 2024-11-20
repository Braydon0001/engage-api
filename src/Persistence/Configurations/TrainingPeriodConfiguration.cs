namespace Engage.Persistence.Configurations;

class TrainingPeriodConfiguration : IEntityTypeConfiguration<TrainingPeriod>
{
    public void Configure(EntityTypeBuilder<TrainingPeriod> builder)
    {
        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();
    }
}
