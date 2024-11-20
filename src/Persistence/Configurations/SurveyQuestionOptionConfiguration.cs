using Engage.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Engage.Persistence.Configurations
{
    public class SurveyQuestionOptionConfiguration : IEntityTypeConfiguration<SurveyQuestionOption>
    {
        public void Configure(EntityTypeBuilder<SurveyQuestionOption> builder)
        {
            builder.HasIndex(x => new { x.SurveyQuestionId, x.DisplayOrder } )
                .IsUnique();

            builder.Property(e => e.Option)
                .IsRequired()
                .HasMaxLength(300);
            
        }
    }
}
