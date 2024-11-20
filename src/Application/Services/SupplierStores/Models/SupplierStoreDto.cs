namespace Engage.Application.Services.SupplierStores.Models;

public class SupplierStoreDto : IMapFrom<SupplierStore>
{
    public int Id { get; set; }
    public int SupplierId { get; set; }
    public string SupplierName { get; set; }
    public int StoreId { get; set; }
    public string StoreName { get; set; }
    public int EngageSubGroupId { get; set; }
    public string EngageSubGroupName { get; set; }
    public int StoreDepartmentId { get; set; }
    public string StoreDepartmentName { get; set; }
    public int FrequencyTypeId { get; set; }
    public string FrequencyTypeName { get; set; }
    public int SupplierRegionId { get; set; }
    public string SupplierRegionName { get; set; }
    public int? SupplierSubRegionId { get; set; }
    public string SupplierSubRegionName { get; set; }
    public int Frequency { get; set; }
    public string AccountNumber { get; set; }
    public string Note { get; set; }
    public bool Disabled { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierStore, SupplierStoreDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierStoreId))
            .ForMember(d => d.SupplierName, opt => opt.MapFrom(s => s.Supplier.Name))
            .ForMember(d => d.StoreName, opt => opt.MapFrom(s => s.Store.Name))
            .ForMember(d => d.EngageSubGroupName, opt => opt.MapFrom(s => s.EngageSubGroup.Name))
            .ForMember(d => d.StoreDepartmentId, opt => opt.MapFrom(s => s.EngageSubGroup.StoreDepartmentId))
            .ForMember(d => d.StoreDepartmentName, opt => opt.MapFrom(s => s.EngageSubGroup.StoreDepartment.Name))
            .ForMember(d => d.FrequencyTypeName, opt => opt.MapFrom(s => s.FrequencyType.Name))
            .ForMember(d => d.SupplierRegionName, opt => opt.MapFrom(s => s.SupplierRegion.Name))
            .ForMember(d => d.SupplierSubRegionName, opt => opt.MapFrom(s => s.SupplierSubRegionId.HasValue ? s.SupplierSubRegion.Name : string.Empty));
    }
}
