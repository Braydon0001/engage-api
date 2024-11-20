using Engage.Application.Services.EngageSubRegions.Queries;

namespace Engage.Application.Services.Stores.Models;

public class StoreVm : IMapFrom<Store>
{
    public int Id { get; set; }
    public OptionDto ParentStoreId { get; set; }
    public OptionDto StoreClusterId { get; set; }
    public OptionDto StoreFormatId { get; set; }
    public OptionDto StoreGroupId { get; set; }
    public OptionDto StoreLSMId { get; set; }
    public OptionDto StoreMediaGroupId { get; set; }
    public OptionDto StoreSparRegionId { get; set; }
    public OptionDto StoreTypeId { get; set; }
    public OptionDto StoreLocationTypeId { get; set; }
    public OptionDto EngageRegionId { get; set; }
    public EngageSubRegionOption EngageSubRegionId { get; set; }
    public List<OptionDto> StoreDepartmentIds { get; set; }
    public List<OptionDto> StoreConceptIds { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string CatManStoreCode { get; set; }
    public bool Disabled { get; set; }
    public bool IsHalaal { get; set; }
    public bool IsNotServiced { get; set; }
    public bool Deleted { get; set; }
    public string StoreImageUrl { get; set; }
    public string StoreTypeImageUrl { get; set; }
    public void Mapping(AutoMapper.Profile profile)
    {
        profile.CreateMap<Store, StoreVm>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(d => d.StoreId))
            .ForMember(dest => dest.ParentStoreId, opt => opt.MapFrom(d => d.ParentStoreId.HasValue ? new OptionDto(d.ParentStoreId.Value, d.ParentStore.Name) : null))
            .ForMember(dest => dest.StoreTypeImageUrl, opt => opt.MapFrom(d => d.StoreType.ImageUrl))
            .ForMember(dest => dest.StoreClusterId, opt => opt.MapFrom(d => new OptionDto(d.StoreClusterId, d.StoreCluster.Name)))
            .ForMember(dest => dest.StoreFormatId, opt => opt.MapFrom(d => new OptionDto(d.StoreFormatId, d.StoreFormat.Name)))
            .ForMember(dest => dest.StoreGroupId, opt => opt.MapFrom(d => new OptionDto(d.StoreGroupId, d.StoreGroup.Name)))
            .ForMember(dest => dest.StoreLSMId, opt => opt.MapFrom(d => new OptionDto(d.StoreLSMId, d.StoreLSM.Name)))
            .ForMember(dest => dest.StoreMediaGroupId, opt => opt.MapFrom(d => new OptionDto(d.StoreMediaGroupId, d.StoreMediaGroup.Name)))
            .ForMember(dest => dest.StoreSparRegionId, opt => opt.MapFrom(d => new OptionDto(d.StoreSparRegionId, d.StoreSparRegion.Name)))
            .ForMember(dest => dest.StoreTypeId, opt => opt.MapFrom(d => new OptionDto(d.StoreTypeId, d.StoreType.Name)))
            .ForMember(dest => dest.StoreLocationTypeId, opt => opt.MapFrom(d => new OptionDto(d.StoreLocationTypeId, d.StoreLocationType.Name)))
            .ForMember(dest => dest.EngageRegionId, opt => opt.MapFrom(d => new OptionDto(d.EngageRegionId, d.EngageRegion.Name)))
            .ForMember(d => d.StoreDepartmentIds, opt => opt.MapFrom(s => s.StoreStoreDepartments.Select(o => new OptionDto(o.StoreDepartmentId, o.StoreDepartment.Name))))
            .ForMember(d => d.StoreConceptIds, opt => opt.MapFrom(s => s.StoreConceptLevels.Select(o => new OptionDto(o.StoreConceptId, o.StoreConcept.Name))))
            .ForMember(d => d.EngageSubRegionId, opt => opt.MapFrom(s => s.EngageSubRegion));
    }
}
