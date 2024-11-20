namespace Engage.Persistence.Configurations
{
    public class SurveyQuestionConfiguration : IEntityTypeConfiguration<SurveyQuestion>
    {
        public void Configure(EntityTypeBuilder<SurveyQuestion> builder)
        {
            //builder.HasIndex(x => new { x.SurveyId, x.DisplayOrder } )
            //    .IsUnique();

            builder.Property(e => e.Question)
                .IsRequired()
                .HasMaxLength(300);

            builder.HasMany(x => x.Rules)
                .WithOne(x => x.Question)
                .HasForeignKey(x => x.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
