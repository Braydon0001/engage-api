using Engage.Application.Services.StoreAssetConditions.Queries;
using Engage.Application.Services.StoreAssetStatuses.Queries;
using Engage.Application.Services.StoreAssetTypeContacts.Queries;

namespace Engage.Application.Services.StoreAssets.Models;

public class StoreAssetVm : IMapFrom<StoreAsset>
{
    public int Id { get; set; }
    public OptionDto StoreId { get; set; }
    public OptionDto StoreAssetTypeId { get; set; }
    public OptionDto StoreAssetSubTypeId { get; set; }
    public OptionDto StoreAssetOwnerId { get; set; }
    public StoreAssetStatusOption StoreAssetStatusId { get; set; }
    public StoreAssetConditionOption StoreAssetConditionId { get; set; }
    public List<StoreAssetTypeContactOption> StoreAssetTypeContactIds { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string SerialNumber { get; set; }
    public string EmailAddress { get; set; }
    public string Note { get; set; }
    public DateTime? InstallDate { get; set; }
    public DateTime? UpliftedDate { get; set; }
    public List<JsonFile> StoreAssetContract { get; set; }
    public List<JsonFile> AssetImages { get; set; }
    public List<JsonFile> AssetDocuments { get; set; }
    public ICollection<OptionDto> StoreConceptAttributeIds { get; set; }
    //public List<EntityBlobDto> Blobs { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreAsset, StoreAssetVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.StoreAssetId))
            .ForMember(d => d.StoreId, opt => opt.MapFrom(s => new OptionDto(s.StoreId, s.Store.Name)))
            .ForMember(d => d.StoreAssetTypeId, opt => opt.MapFrom(s => new OptionDto(s.StoreAssetTypeId, s.StoreAssetType.Name)))
            .ForMember(d => d.StoreAssetSubTypeId, opt => opt.MapFrom(s => s.StoreAssetSubTypeId.HasValue ? new OptionDto(s.StoreAssetSubTypeId.Value, s.StoreAssetSubType.Name) : null))
            .ForMember(d => d.StoreAssetOwnerId, opt => opt.MapFrom(s => s.StoreAssetOwnerId.HasValue ? new OptionDto(s.StoreAssetOwnerId.Value, s.StoreAssetOwner.Name) : null))
            .ForMember(d => d.StoreAssetStatusId, opt => opt.MapFrom(s => s.StoreAssetStatus))
        .ForMember(d => d.StoreConceptAttributeIds, opt => opt.MapFrom(s => s.StoreConceptAttributeStoreAssets.Select(e => new OptionDto(e.StoreConceptAttributeId, e.StoreConceptAttribute.Name))))
        .ForMember(d => d.StoreAssetConditionId, opt => opt.MapFrom(s => s.StoreAssetCondition))
        .ForMember(d => d.StoreAssetTypeContactIds, opt => opt.MapFrom(s => s.AssetTypeContacts.Select(x => x.StoreAssetTypeContact).ToList()));
        //.ForMember(d => d.StoreAssetContract, opt => opt.MapFrom(s => s.Files.Where(f => f.Type == "assetContract")))
        //.ForMember(d => d.AssetImages, opt => opt.MapFrom(s => s.Files.Where(f => f.Type == "assetImage")));
        //.ForMember(d => d.Blobs, opt => opt.MapFrom(s => s.AssetImages.OrderByDescending(e => e.AssetImageId).Select(e => new EntityBlobDto(e))));
    }
}
