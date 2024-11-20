namespace Engage.Persistence.Configurations;

public class SurveyConfiguration : IEntityTypeConfiguration<Survey>
{
    public void Configure(EntityTypeBuilder<Survey> builder)
    {
        builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(220);

        builder.Property(e => e.Note)
            .HasMaxLength(300);
    }
}
