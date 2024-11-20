using AutoMapper;
using Engage.Application.Mappings;
using Engage.Domain.Entities;

namespace Engage.Application.Services.SubWarehouses.Commands
{
    public class SubWarehouseCommand : IMapTo<SubWarehouse>
    {
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SubWarehouseCommand, SubWarehouse>();
        }
    }
}
