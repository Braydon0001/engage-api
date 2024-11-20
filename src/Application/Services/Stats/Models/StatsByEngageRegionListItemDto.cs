using AutoMapper;
using Engage.Application.Mappings;
using Engage.Domain.Entities.Views;

namespace Engage.Application.Services.Stats.Models
{
    public class StatsByEngageRegionListItemDto : IMapFrom<StatsByEngageRegionView>
    {
        public int EngageRegionId { get; set; }
        public string EngageRegionName { get; set; }
        public int StoresCount { get; set; }
        public int OrdersLast1Day { get; set; }
        public int OrdersLast7Days { get; set; }
        public int OrdersAll { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<StatsByEngageRegionView, StatsByEngageRegionListItemDto>();
        }
    }
}
