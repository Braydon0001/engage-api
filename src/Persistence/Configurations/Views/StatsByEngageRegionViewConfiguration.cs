using Engage.Domain.Entities.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Engage.Persistence.Configurations.Views
{
    class StatsByEngageRegionViewConfiguration : IEntityTypeConfiguration<StatsByEngageRegionView>
    {
        public void Configure(EntityTypeBuilder<StatsByEngageRegionView> builder)
        {
            builder.HasNoKey().ToView("vw_statsbyengageregion");
        }
    }
}
