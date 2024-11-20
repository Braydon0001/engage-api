using Engage.Domain.Entities.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Engage.Persistence.Configurations.Views
{
    public class SurveysByEmployeeStoreViewConfiguration : IEntityTypeConfiguration<SurveysByEmployeeStoreView>
    {
        public void Configure(EntityTypeBuilder<SurveysByEmployeeStoreView> builder)
        {
            builder.HasNoKey().ToView("vw_surveysbyemployeestore");
        }
    }

    public class SurveysByEmployeePerRegionViewConfiguration2 : IEntityTypeConfiguration<SurveysByEmployeePerRegionView2>
    {
        public void Configure(EntityTypeBuilder<SurveysByEmployeePerRegionView2> builder)
        {
            builder.HasNoKey()
                .ToView("vw_surveysbyemployeeperregion2");
        }
    }

    public class SurveysByEmployeePerStoreViewConfiguration : IEntityTypeConfiguration<SurveysByEmployeePerStoreView>
    {
        public void Configure(EntityTypeBuilder<SurveysByEmployeePerStoreView> builder)
        {
            builder.HasNoKey()
                .ToView("vw_surveysbyemployeeperstore");
        }
    }

    public class SurveysByEmployeePerStoreViewConfiguration_ : IEntityTypeConfiguration<SurveysByEmployeePerStoreView_>
    {
        public void Configure(EntityTypeBuilder<SurveysByEmployeePerStoreView_> builder)
        {
            builder.HasNoKey()
                .ToView("vw_surveysbyemployeeperstore_");
        }
    }
    
    public class SurveysByEmployeePerStoreFormatViewConfiguration : IEntityTypeConfiguration<SurveysByEmployeePerStoreFormatView>
    {
        public void Configure(EntityTypeBuilder<SurveysByEmployeePerStoreFormatView> builder)
        {
            builder.HasNoKey()
                .ToView("vw_surveysbyemployeeperstoreformat");
        }
    }

    public class SurveysByEmployeePerRegionViewConfiguration : IEntityTypeConfiguration<SurveysByEmployeePerRegionView>
    {
        public void Configure(EntityTypeBuilder<SurveysByEmployeePerRegionView> builder)
        {
            builder.HasNoKey()
                .ToView("vw_surveysbyemployeeperregion");
        }
    }

    public class SurveysByEmployeeSubGroupPerStoreViewConfiguration : IEntityTypeConfiguration<SurveysByEmployeeSubGroupPerStoreView>
    {
        public void Configure(EntityTypeBuilder<SurveysByEmployeeSubGroupPerStoreView> builder)
        {
            builder.HasNoKey()
                .ToView("vw_surveysbyemployeesubgroupperstore");
        }
    }

    public class SurveysByEmployeeSubGroupPerStoreFormatViewConfiguration : IEntityTypeConfiguration<SurveysByEmployeeSubGroupPerStoreFormatView>
    {
        public void Configure(EntityTypeBuilder<SurveysByEmployeeSubGroupPerStoreFormatView> builder)
        {
            builder.HasNoKey()
                .ToView("vw_surveysbyemployeesubgroupperstoreformat");
        }
    }

    public class SurveysByEmployeeSubGroupPerRegionViewConfiguration : IEntityTypeConfiguration<SurveysByEmployeeSubGroupPerRegionView>
    {
        public void Configure(EntityTypeBuilder<SurveysByEmployeeSubGroupPerRegionView> builder)
        {
            builder.HasNoKey()
                .ToView("vw_surveysbyemployeesubgroupperregion");
        }
    }
}
