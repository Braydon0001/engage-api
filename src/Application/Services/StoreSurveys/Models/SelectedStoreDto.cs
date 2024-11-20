using AutoMapper;
using Engage.Application.Mappings;
using Engage.Domain.Entities;

namespace Engage.Application.Services.StoreSurveys.Models
{
    public class SelectedStoreDto: IMapFrom<Store>
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool Selected { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Store, SelectedStoreDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.StoreId));
        }
    }
}
