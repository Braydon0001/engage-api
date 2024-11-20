namespace Engage.Application.Services.SupplierStores.Models;


public class SupplierStoreVm : IMapFrom<SupplierStore>
{
    public int Id { get; set; }
    public OptionDto SupplierId { get; set; }
    public OptionDto SupplierRegionId { get; set; }
    public OptionDto SupplierSubRegionId { get; set; }
    public OptionDto StoreId { get; set; }
    public OptionDto EngageSubGroupId { get; set; }
    public OptionDto FrequencyTypeId { get; set; }
    public int Frequency { get; set; }
    public string AccountNumber { get; set; }
    public string Note { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierStore, SupplierStoreVm>()
            .ForMember(d => d.Id, opts => opts.MapFrom(s => s.SupplierStoreId))
            .ForMember(d => d.SupplierId, opts => opts.MapFrom(s => new OptionDto { Id = s.SupplierId, Name = s.Supplier.Name }))
            .ForMember(d => d.SupplierRegionId, opts => opts.MapFrom(s => new OptionDto { Id = s.SupplierRegionId, Name = s.SupplierRegion.Name }))
            .ForMember(d => d.SupplierSubRegionId, opt => opt.MapFrom(s => s.SupplierSubRegionId.HasValue
                                                                 ? new OptionDto(s.SupplierSubRegionId.Value, s.SupplierSubRegion.Name)
                                                                 : null))
            .ForMember(d => d.StoreId, opts => opts.MapFrom(s => new OptionDto { Id = s.StoreId, Name = s.Store.Name }))
            .ForMember(d => d.EngageSubGroupId, opts => opts.MapFrom(s => new OptionDto { Id = s.EngageSubGroupId, Name = s.EngageSubGroup.Name }))
            .ForMember(d => d.FrequencyTypeId, opts => opts.MapFrom(s => new OptionDto { Id = s.FrequencyTypeId, Name = s.FrequencyType.Name }));
    }
}
