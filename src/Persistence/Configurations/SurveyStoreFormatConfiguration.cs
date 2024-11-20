using Engage.Domain.Entities.LinkEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Engage.Persistence.Configurations
{
    public class SurveyStoreFormatConfiguration : IEntityTypeConfiguration<SurveyStoreFormat>
    {
        public void Configure(EntityTypeBuilder<SurveyStoreFormat> builder)
        {
            builder.HasKey(e => new { e.SurveyId, e.StoreFormatId })
                .IsClustered(false);

            builder.HasOne(x => x.Survey)
                .WithMany(s => s.SurveyStoreFormats)
                .HasForeignKey(x => x.SurveyId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(x => x.StoreFormat)
                .WithMany(s => s.SurveyStoreFormats)
                .HasForeignKey(x => x.StoreFormatId)
                .OnDelete(DeleteBehavior.ClientSetNull);

        }
    }
}
