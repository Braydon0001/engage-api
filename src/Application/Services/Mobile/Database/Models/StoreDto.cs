namespace Engage.Application.Mobile.Database.Models;

public class StoreDto : IMapFrom<Store>
{
    public int Id { get; set; }
    public int? ParentStoreId { get; set; }
    public int StakeholderId { get; set; }
    public int StoreClusterId { get; set; }
    public int StoreFormatId { get; set; }
    public int StoreGroupId { get; set; }
    public int StoreLSMId { get; set; }
    public int StoreMediaGroupId { get; set; }
    public int StoreSparRegionId { get; set; }
    public int StoreTypeId { get; set; }
    public int? StoreLocationTypeId { get; set; }
    public int EngageRegionId { get; set; }
    public int EngageLocationId { get; set; }
    public int? PrimaryLocationId { get; set; }
    public int? PrimaryContactId { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string CatManStoreCode { get; set; }
    public bool Disabled { get; set; }
    public string StoreImageUrl { get; set; }
    public string StoreTypeImageUrl { get; set; }
    public ICollection<OptionDto> StoreDepartments { get; set; }
    public ICollection<OptionDto> StoreConceptLevels { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Store, StoreDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(d => d.StoreId))
            .ForMember(dest => dest.StoreTypeImageUrl, opt => opt.MapFrom(d => d.StoreType.ImageUrl))
            .ForMember(d => d.StoreDepartments, opt => opt.MapFrom(s => s.StoreStoreDepartments
                                                                       .Select(s => s.StoreDepartment)
                                                                       .Select(s => new OptionDto() { Id = s.Id, Name = s.Name })))
            .ForMember(d => d.StoreConceptLevels, opt => opt.MapFrom(s => s.StoreConceptLevels
                                                                       .Select(s => s.StoreConcept)
                                                                       .Select(s => new OptionDto() { Id = s.Id, Name = s.Name })));
    }
}
