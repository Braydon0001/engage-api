namespace Engage.Application.Services.Stores.Commands;

public class StoreCommand : IMapTo<Store>
{
    public int StoreFormatId { get; set; }
    public int StoreClusterId { get; set; }
    public int StoreGroupId { get; set; } = 1;
    public int StoreLSMId { get; set; }
    public int StoreMediaGroupId { get; set; } = 1;
    public int StoreTypeId { get; set; }
    public int StoreSparRegionId { get; set; } = 1;
    public int? StoreLocationTypeId { get; set; } = 1;
    public int EngageRegionId { get; set; }
    public int? EngageSubRegionId { get; set; }
    public int? PrimaryLocationId { get; set; }
    public int? PrimaryContactId { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string CatManStoreCode { get; set; }
    public string StoreImageUrl { get; set; }
    public bool Disabled { get; set; }
    public bool IsHalaal { get; set; }
    public bool IsNotServiced { get; set; }
    public List<int> StoreDepartmentIds { get; set; }
    public List<int> StoreConceptIds { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreCommand, Store>();
    }
}
