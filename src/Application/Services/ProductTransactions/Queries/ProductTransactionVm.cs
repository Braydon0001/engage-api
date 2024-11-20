// auto-generated
using Engage.Application.Services.Employees.Queries;
using Engage.Application.Services.ProductPeriods.Queries;
using Engage.Application.Services.Products.Queries;
using Engage.Application.Services.ProductTransactionStatuses.Queries;
using Engage.Application.Services.ProductTransactionTypes.Queries;
using Engage.Application.Services.ProductWarehouses.Queries;

namespace Engage.Application.Services.ProductTransactions.Queries;

public class ProductTransactionVm : IMapFrom<ProductTransaction>
{
    public int Id { get; set; }
    public ProductOption ProductId { get; set; }
    public ProductTransactionTypeOption ProductTransactionTypeId { get; set; }
    public ProductTransactionStatusOption ProductTransactionStatusId { get; set; }
    public ProductPeriodOption ProductPeriodId { get; set; }
    public ProductWarehouseOption ProductWarehouseId { get; set; }
    public EmployeeOption EmployeeId { get; set; }
    public float Quantity { get; set; }
    public decimal Price { get; set; }
    public DateTime TransactionDate { get; set; }
    public string Note { get; set; }
    public List<JsonFile> Files { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductTransaction, ProductTransactionVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductTransactionId))
               .ForMember(d => d.ProductId, opt => opt.MapFrom(s => s.Product))
               .ForMember(d => d.ProductTransactionTypeId, opt => opt.MapFrom(s => s.ProductTransactionType))
               .ForMember(d => d.ProductTransactionStatusId, opt => opt.MapFrom(s => s.ProductTransactionStatus))
               .ForMember(d => d.ProductPeriodId, opt => opt.MapFrom(s => s.ProductPeriod))
               .ForMember(d => d.EmployeeId, opt => opt.MapFrom(s => s.Employee))
               .ForMember(d => d.ProductWarehouseId, opt => opt.MapFrom(s => s.ProductWarehouse));
    }
}
