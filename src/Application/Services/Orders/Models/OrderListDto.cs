namespace Engage.Application.Services.Orders.Models;

public class OrderListDto : IMapFrom<Order>
{
    public int Id { get; set; }
    public int OrderTypeId { get; set; }
    public string OrderTypeName { get; set; }
    public int OrderStatusId { get; set; }
    public string OrderStatusName { get; set; }
    public int? OrderTemplateId { get; set; }
    public string OrderTemplateName { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public DateTime? SubmittedDate { get; set; }
    public string ProcessedBy { get; set; }
    public DateTime? ProcessedDate { get; set; }
    public bool Deleted { get; set; }
    public string DeletedBy { get; set; }
    public DateTime? DeletedDate { get; set; }
    public string OrderReference { get; set; }
    public int StoreId { get; set; }
    public string StoreCode { get; set; }
    public string StoreName { get; set; }
    public int SupplierId { get; set; }
    public string SupplierName { get; set; }
    public int EngageRegionId { get; set; }
    public string EngageRegionName { get; set; }
    public int DistributionCenterId { get; set; }
    public string DistributionCenterCode { get; set; }
    public string DistributionCenterName { get; set; }
    public string VATNumber { get; set; }
    public string AccountNumber { get; set; }
    public string Email { get; set; }
    public string Contact { get; set; }
    public string Address { get; set; }
    public string UserName { get; set; }
    public string OrderDepartments { get; set; }
    public List<JsonFile> Files { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Order, OrderListDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.OrderId))
            .ForMember(d => d.SupplierName, opt => opt.MapFrom(s => !s.SupplierId.HasValue || s.SupplierId.Value == 0 ? "ENGAGE GENERIC SUPPLIER" : s.Supplier.Name))
            .ForMember(d => d.EngageRegionId, opt => opt.MapFrom(s => s.Store.EngageRegionId))
            .ForMember(d => d.EngageRegionName, opt => opt.MapFrom(s => s.Store.EngageRegion.Name))
            .ForMember(d => d.AccountNumber, opt => opt.MapFrom(s => s.DCAccountNo))
            .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.CreatedBy))
            .ForMember(d => d.OrderDepartments, opt => opt.MapFrom(s => string.Join(", ", s.OrderEngageDepartments.Select(s => s.EngageDepartment.Name))));
    }
}

public class OrderSubTotalDto : OrderListDto, IMapFrom<Order>
{
    public int QuantitySum { get; set; }

    public new void Mapping(Profile profile)
    {
        profile.CreateMap<Order, OrderSubTotalDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.OrderId))
            .ForMember(d => d.SupplierName, opt => opt.MapFrom(s => !s.SupplierId.HasValue || s.SupplierId.Value == 0 ? "ENGAGE GENERIC SUPPLIER" : s.Supplier.Name))
            .ForMember(d => d.EngageRegionId, opt => opt.MapFrom(s => s.Store.EngageRegionId))
            .ForMember(d => d.EngageRegionName, opt => opt.MapFrom(s => s.Store.EngageRegion.Name))
            .ForMember(d => d.AccountNumber, opt => opt.MapFrom(s => s.DCAccountNo))
            .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.CreatedBy))
            .ForMember(d => d.OrderDepartments, opt => opt.MapFrom(s => string.Join(", ", s.OrderEngageDepartments.Select(s => s.EngageDepartment.Name))))
            .ForMember(d => d.QuantitySum, opt => opt.MapFrom(s => s.OrderSkus.Where(e => e.Deleted == false).Sum(e => e.Quantity)));
    }
}