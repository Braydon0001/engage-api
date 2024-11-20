using Engage.Application.Services.OrderSkus.Models;

namespace Engage.Application.Services.Orders.Models;

public class OrderVM
{
    public OrderDto Order { get; internal set; }
    public OptionDto StoreIdOption { get; set; }
    public OptionDto OrderStatusIdOption { get; set; }
    public OptionDto DistributionCenterIdOption { get; set; }
    public OptionDto SupplierIdOption { get; set; }
    public ICollection<OptionDto> OrderTypes { get; set; }
    public ICollection<OptionDto> OrderStatuses { get; set; }
    public ICollection<OptionDto> UnassignedEngageDepartments { get; set; }
    public OrderSkusByQuantityTypeDto OrderSkus { get; set; }
}

// vm2
public class OrderVm : IMapFrom<Order>
{
    public int Id { get; set; }
    public OptionDto OrderTypeId { get; set; }
    public OptionDto OrderStatusId { get; set; }
    public OptionDto StoreId { get; set; }
    public CascadingOptionDto DistributionCenterId { get; set; }
    public OptionDto SupplierId { get; set; }
    public OptionDto OrderTemplateId { get; set; }
    public string OrderTemplateNote { get; set; }
    public DateTime OrderTemplateStartDate { get; set; }
    public DateTime? OrderTemplateEndDate { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public DateTime? SubmittedDate { get; set; }
    public DateTime? ProcessedDate { get; set; }
    public string DCAccountNo { get; set; }
    public string OrderNo { get; set; }
    public string OrderReference { get; set; }
    public string Comment { get; set; }
    public string Note { get; set; }
    public string VATNumber { get; set; }
    public string AccountNumber { get; set; }
    public string Email { get; set; }
    public string Contact { get; set; }
    public string Address { get; set; }
    public List<JsonFile> Files { get; set; }
    public List<OptionDto> EngageDepartmentIds { get; set; }
    public ListResult<OrderSkuListItemDto> OrderSkus { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Order, OrderVm>()
            .ForMember(d => d.Id, opts => opts.MapFrom(s => s.OrderId))
            .ForMember(d => d.OrderTypeId, opts => opts.MapFrom(s => new OptionDto { Id = s.OrderTypeId, Name = s.OrderType.Name }))
            .ForMember(d => d.OrderStatusId, opts => opts.MapFrom(s => new OptionDto { Id = s.OrderStatusId, Name = s.OrderStatus.Name }))
            .ForMember(d => d.StoreId, opts => opts.MapFrom(s => new OptionDto { Id = s.StoreId, Name = s.Store.Name }))
            .ForMember(d => d.DistributionCenterId, opts => opts.MapFrom(s => new CascadingOptionDto
            {
                ParentId = s.StoreId,
                Id = s.DistributionCenterId,
                Name = $"{s.DistributionCenter.Name} / {s.DCAccountNo}"
            }))
            .ForMember(d => d.SupplierId, opt => opt.Ignore())
            .ForMember(d => d.OrderTemplateId, opts => opts.MapFrom(s => s.OrderTemplateId.HasValue ? new OptionDto(s.OrderTemplateId.Value, s.OrderTemplate.Name) : null))
            .ForMember(d => d.EngageDepartmentIds, opt => opt.MapFrom(s => s.OrderEngageDepartments.Select(s => new OptionDto() { Id = s.EngageDepartment.Id, Name = s.EngageDepartment.Name })))
            .ForMember(d => d.OrderSkus, opts => opts.Ignore());

    }

}
