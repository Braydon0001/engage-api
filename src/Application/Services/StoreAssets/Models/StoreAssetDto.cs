using Engage.Application.Services.StoreConceptAttributeStoreAssets.Models;

namespace Engage.Application.Services.StoreAssets.Models;

public class StoreAssetDto : IMapFrom<StoreAsset>
{
    public int Id { get; set; }
    public int StoreId { get; set; }
    public string StoreName { get; set; }
    public int? StoreAssetOwnerId { get; set; }
    public string StoreAssetOwnerName { get; set; }
    public int StoreAssetTypeId { get; set; }
    public string StoreAssetTypeName { get; set; }
    public int? StoreAssetSubTypeId { get; set; }
    public int? StoreAssetConditionId { get; set; }
    public string StoreAssetSubTypeName { get; set; }
    public int StoreAssetStatusId { get; set; }
    public string StoreAssetStatusName { get; set; }
    public string StoreAssetConditionName { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Note { get; set; }
    public string SerialNumber { get; set; }
    public string EmailAddress { get; set; }
    public string StoreAssetTypeContacts { get; set; }
    public DateTime? InstallDate { get; set; }
    public DateTime? UpliftDate { get; set; }
    public List<StoreConceptAttributeStoreAssetDto> StoreConceptAttributeStoreAssets { get; set; }
    public List<JsonFile> Files { get; set; }
    public bool HasContract { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreAsset, StoreAssetDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.StoreAssetId))
            .ForMember(d => d.HasContract, opt => opt.MapFrom(s => s.HasContract))
            .ForMember(d => d.StoreAssetTypeContacts, opt => opt.MapFrom(s => string.Join(", ", s.AssetTypeContacts.Select(x => x.StoreAssetTypeContact.EmailAddress))));

        profile.CreateMap<StoreConceptAttributeStoreAssetDto, StoreAssetDto>();
    }
}
