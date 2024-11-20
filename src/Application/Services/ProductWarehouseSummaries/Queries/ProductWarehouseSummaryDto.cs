// auto-generated
namespace Engage.Application.Services.ProductWarehouseSummaries.Queries;

public class ProductWarehouseSummaryDto : IMapFrom<ProductWarehouseSummary>
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int ProductWarehouseId { get; set; }
    public string ProductWarehouseName { get; set; }
    public int ProductPeriodId { get; set; }
    public string ProductPeriodName { get; set; }
    public float Quantity { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductWarehouseSummary, ProductWarehouseSummaryDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductWarehouseSummaryId));
    }
}
