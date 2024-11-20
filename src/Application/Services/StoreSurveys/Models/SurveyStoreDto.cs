using AutoMapper;
using Engage.Application.Mappings;
using Engage.Domain.Entities.LinkEntities;

namespace Engage.Application.Services.StoreSurveys.Models
{
    public class SurveyStoreDto : IMapFrom<SurveyStore>
    {
        public int SurveyId { get; set; }
        public int StoreId { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<SurveyStore, SurveyStoreDto>();
        }
    }
}
