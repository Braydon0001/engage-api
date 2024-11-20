using Engage.Domain.Entities.LinkEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Engage.Persistence.Configurations
{
    public class SurveyStoreConfiguration : IEntityTypeConfiguration<SurveyStore>
    {
        public void Configure(EntityTypeBuilder<SurveyStore> builder)
        {
            builder.HasKey(e => new { e.SurveyId, e.StoreId })
                .IsClustered(false);

            builder.HasOne(x => x.Survey)
                .WithMany(s => s.SurveyStores)
                .HasForeignKey(x => x.SurveyId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(x => x.Store)
                .WithMany(s => s.SurveyStores)
                .HasForeignKey(x => x.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull);

        }
    }
}
