
using Engage.Application.Services.ProductOrderStatuses.Queries;
using Engage.Application.Services.ProductOrderTypes.Queries;
using Engage.Application.Services.ProductPeriods.Queries;
using Engage.Application.Services.ProductSuppliers.Queries;
using Engage.Application.Services.ProductWarehouses.Queries;

namespace Engage.Application.Services.ProductOrders.Queries;

public class ProductOrderVm : IMapFrom<ProductOrder>
{
    public int Id { get; set; }
    public string OrderNumber { get; set; }
    public ProductOrderStatusOption ProductOrderStatusId { get; set; }
    public ProductWarehouseOption ProductWarehouseId { get; set; }
    public ProductWarehouseOption ProductWarehouseOutId { get; set; }
    public ProductOrderTypeOption ProductOrderTypeId { get; set; }
    public ProductPeriodOption ProductPeriodId { get; set; }
    public ProductSupplierOption ProductSupplierId { get; set; }
    public DateTime OrderDate { get; set; }
    public List<JsonFile> Files { get; set; }
    public List<JsonText> Note { get; set; }
    public string RejectReason { get; set; }
    public int OrderLineCount { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductOrder, ProductOrderVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductOrderId))
               .ForMember(d => d.ProductOrderStatusId, opt => opt.MapFrom(s => s.ProductOrderStatus))
               .ForMember(d => d.ProductWarehouseId, opt => opt.MapFrom(s => s.ProductWarehouse))
               .ForMember(d => d.ProductWarehouseOutId, opt => opt.MapFrom(s => s.ProductWarehouseOut))
               .ForMember(d => d.ProductOrderTypeId, opt => opt.MapFrom(s => s.ProductOrderType))
               .ForMember(d => d.ProductPeriodId, opt => opt.MapFrom(s => s.ProductPeriod))
               .ForMember(d => d.ProductSupplierId, opt => opt.MapFrom(s => s.ProductSupplier));
    }
}
