namespace Engage.Application.Services.PaymentLines.Queries;

public class PaymentLineDto : IMapFrom<PaymentLine>
{
    public int Id { get; init; }
    public int PaymentId { get; init; }
    public int ExpenseTypeId { get; init; }
    public string ExpenseTypeName { get; init; }
    public string Employees { get; init; }
    public string CostCenters { get; init; }
    public string Divisions { get; init; }
    public string SubDepartments { get; init; }
    public float Amount { get; init; }
    public float VatAmount { get; init; }
    public float TotalAmount { get; init; }
    public bool IsVat { get; set; }
    public bool IsSplitAmount { get; set; }
    public bool HasQuote { get; set; }
    public bool HasInvoice { get; set; }
    public string Note { get; set; }
    public string CreatedBy { get; set; }
    public DateTime Created { get; set; }
    public List<JsonFile> Files { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PaymentLine, PaymentLineDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.PaymentLineId))
               .ForMember(d => d.TotalAmount, opt => opt.MapFrom(s => s.Amount + s.VatAmount))
               .ForMember(d => d.Employees, opt => opt.MapFrom(s => string.Join(", ", s.Employees.Select(s => s.Employee.FirstName + " " + s.Employee.LastName + " (" + s.Employee.Code + ")"))))
               .ForMember(d => d.CostCenters, opt => opt.MapFrom(s => string.Join(", ", s.CostCenters.Select(s => s.CostCenter.Name))))
               .ForMember(d => d.Divisions, opt => opt.MapFrom(s => string.Join(", ", s.EmployeeDivisions.Select(s => s.EmployeeDivision.Name))))
               .ForMember(d => d.SubDepartments, opt => opt.MapFrom(s => string.Join(", ", s.SubDepartments.Select(s => s.CostSubDepartment.Name))));
    }
}
