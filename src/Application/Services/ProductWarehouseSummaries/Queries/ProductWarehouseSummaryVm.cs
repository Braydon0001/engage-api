// auto-generated
using Engage.Application.Services.Products.Queries;
using Engage.Application.Services.ProductWarehouses.Queries;
using Engage.Application.Services.ProductPeriods.Queries;

namespace Engage.Application.Services.ProductWarehouseSummaries.Queries;

public class ProductWarehouseSummaryVm : IMapFrom<ProductWarehouseSummary>
{
    public int Id { get; set; }
    public ProductOption ProductId { get; set; }
    public ProductWarehouseOption ProductWarehouseId { get; set; }
    public ProductPeriodOption ProductPeriodId { get; set; }
    public float Quantity { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductWarehouseSummary, ProductWarehouseSummaryVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductWarehouseSummaryId))
               .ForMember(d => d.ProductId, opt => opt.MapFrom(s => s.Product))
               .ForMember(d => d.ProductWarehouseId, opt => opt.MapFrom(s => s.ProductWarehouse))
               .ForMember(d => d.ProductPeriodId, opt => opt.MapFrom(s => s.ProductPeriod));
    }
}
