namespace Engage.Application.Services.ProductTransactions.Queries;

public class ProductTransactionByProductWarehouseDto : IMapFrom<ProductTransaction>
{
    public int Id { get; set; }
    public int ProductTransactionTypeId { get; set; }
    public string ProductTransactionTypeName { get; set; }
    public float Quantity { get; set; }
    public string EmployeeName { get; set; }
    public DateTime TransactionDate { get; set; }
    public string Note { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductTransaction, ProductTransactionByProductWarehouseDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductTransactionId))
            .ForMember(d => d.EmployeeName, opt => opt.MapFrom(s => s.EmployeeId != null
                    ? s.Employee.FirstName + " " + s.Employee.LastName + " - " + s.Employee.Code : ""));
    }
}
