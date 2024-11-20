using AutoMapper;
using Engage.Application.Mappings;
using Engage.Domain.Entities;

namespace Engage.Application.Services.SubWarehouses.Models
{
    public class SubWarehouseDto : IMapFrom<SubWarehouse>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SubWarehouse, SubWarehouseDto>()
                .ForMember(d => d.Id, opts => opts.MapFrom(s => s.SubWarehouseId));
        }
    }
}
