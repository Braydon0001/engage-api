namespace Engage.Application.Services.ProductWarehouseSummaries.Queries;

public class ProductWarehouseSummaryStockReportDto
{
    public int ProductWarehouseId { get; set; }
    public string ProductWarehouseName { get; set; }
    public string EngageLogo { get; set; }
    public List<ProductWarehouseSummaryStockReportProductDto> Products { get; set; }
}

public class ProductWarehouseSummaryStockReportProductDto : IMapFrom<Product>
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int ProductMasterId { get; set; }
    public string ProductMasterName { get; set; }
    public float CurrentStock { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Product, ProductWarehouseSummaryStockReportProductDto>()
            .ForMember(d => d.ProductName, opt => opt.MapFrom(s => s.Name))
            .ForMember(d => d.CurrentStock, opt => opt.Ignore());
    }
}

public class ProductWarehouseSummaryStockReportTransactionDto : IMapFrom<ProductTransaction>
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int ProductPeriodId { get; set; }
    public string ProductPeriodName { get; set; }
    public float Quantity { get; set; }
    public decimal Price { get; set; }
    public int ProductMasterId { get; set; }
    public string ProductMasterName { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductTransaction, ProductWarehouseSummaryStockReportTransactionDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductTransactionId))
            .ForMember(d => d.ProductMasterId, opt => opt.MapFrom(s => s.Product.ProductMasterId))
            .ForMember(d => d.ProductMasterName, opt => opt.MapFrom(s => s.Product.ProductMaster.Name))
            .ForMember(d => d.ProductName, opt => opt.MapFrom(S => S.Product.Name));
    }
}

public class ProductWarehouseSummaryStockReportStockDto : IMapFrom<ProductWarehouseSummary>
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int ProductWarehouseId { get; set; }
    public string ProductWarehouseName { get; set; }
    public int ProductPeriodId { get; set; }
    public string ProductPeriodName { get; set; }
    public int ProductMasterId { get; set; }
    public string ProductMasterName { get; set; }
    public float Quantity { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductWarehouseSummary, ProductWarehouseSummaryStockReportStockDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductWarehouseSummaryId))
               .ForMember(d => d.ProductMasterId, opt => opt.MapFrom(s => s.Product.ProductMasterId))
               .ForMember(d => d.ProductMasterName, opt => opt.MapFrom(s => s.Product.ProductMaster.Name));
    }
}