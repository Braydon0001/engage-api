using Engage.Domain.Entities;
using Engage.Domain.Entities.LinkEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Engage.Persistence.Configurations
{
    public class SurveyQuestionFalseReasonConfiguration : IEntityTypeConfiguration<SurveyQuestionFalseReason>
    {
        public void Configure(EntityTypeBuilder<SurveyQuestionFalseReason> builder)
        {
            builder.HasKey(e => new { e.SurveyQuestionId, e.QuestionFalseReasonId })
                .IsClustered(false);

            builder.HasOne(x => x.SurveyQuestion)
                .WithMany(s => s.SurveyQuestionFalseReasons)
                .HasForeignKey(x => x.SurveyQuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(x => x.QuestionFalseReason)
                .WithMany(s => s.SurveyQuestionFalseReasons)
                .HasForeignKey(x => x.QuestionFalseReasonId)
                .OnDelete(DeleteBehavior.ClientSetNull);

        }
    }
}
