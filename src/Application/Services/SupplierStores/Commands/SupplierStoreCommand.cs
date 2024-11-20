namespace Engage.Application.Services.SupplierStores.Commands;

public class SupplierStoreCommand : IMapTo<SupplierStore>
{
    public int SupplierId { get; set; }
    public int SupplierRegionId { get; set; }
    public int? SupplierSubRegionId { get; set; }
    public int StoreId { get; set; }
    public int EngageSubGroupId { get; set; }
    public int FrequencyTypeId { get; set; }
    public int Frequency { get; set; }
    public string Note { get; set; }
    public string AccountNumber { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierStoreCommand, SupplierStore>();
    }
}
