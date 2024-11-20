// auto-generated
namespace Engage.Application.Services.ProductTransactions.Queries;

public class ProductTransactionDto : IMapFrom<ProductTransaction>
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int ProductTransactionTypeId { get; set; }
    public string ProductTransactionTypeName { get; set; }
    public int ProductTransactionStatusId { get; set; }
    public string ProductTransactionStatusName { get; set; }
    public int ProductPeriodId { get; set; }
    public string ProductPeriodName { get; set; }
    public int ProductWarehouseId { get; set; }
    public string ProductWarehouseName { get; set; }
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; }
    public float Quantity { get; set; }
    public decimal Price { get; set; }
    public string EngageRegionNames { get; set; }
    public DateTime TransactionDate { get; set; }
    public string Note { get; set; }
    public List<JsonFile> Files { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductTransaction, ProductTransactionDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductTransactionId))
               .ForMember(d => d.EmployeeName, opt => opt.MapFrom
                            (s => s.EmployeeId != null ? s.Employee.FirstName + " " + s.Employee.LastName + " - " + s.Employee.Code : ""))
               .ForMember(d => d.ProductName, opt => opt.MapFrom(S => S.Product.Name))
               .ForMember(d => d.EngageRegionNames, opt => opt.MapFrom(s => s.EngageRegion.Name));
    }
}

public class InventoryExcelReportVm<T>
{
    public int Count { get; set; }
    public object ReportName { get; set; }
    public List<T> Data { get; set; }
    public List<string> ColumnNames { get; set; }
}