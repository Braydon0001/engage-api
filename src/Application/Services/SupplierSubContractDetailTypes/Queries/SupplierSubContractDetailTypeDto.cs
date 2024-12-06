﻿namespace Engage.Application.Services.SupplierSubContractDetailTypes.Queries;

public class SupplierSubContractDetailTypeDto : IMapFrom<SupplierSubContractDetailType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierSubContractDetailType, SupplierSubContractDetailTypeDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierSubContractDetailTypeId));
    }
}
