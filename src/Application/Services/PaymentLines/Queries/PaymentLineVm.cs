using Engage.Application.Services.ExpenseTypes.Queries;

namespace Engage.Application.Services.PaymentLines.Queries;

public class PaymentLineVm : IMapFrom<PaymentLine>
{
    public int Id { get; init; }
    //public PaymentOption PaymentId { get; init; }
    public int PaymentId { get; init; }
    public ExpenseTypeOption ExpenseTypeId { get; init; }
    //public OptionDto VatId { get; set; }
    public float Amount { get; set; }
    public float VatAmount { get; set; }
    public int? Quantity { get; set; }
    public bool IsVat { get; set; }
    public bool IsSplitAmount { get; set; }
    public bool HasQuote { get; set; }
    public bool HasInvoice { get; set; }
    public string Note { get; set; }
    public ICollection<OptionDto> EmployeeIds { get; set; }
    public ICollection<OptionDto> EmployeeDivisionIds { get; set; }
    public ICollection<OptionDto> CostCenterIds { get; set; }
    public ICollection<OptionDto> CostSubDepartmentIds { get; set; }
    public List<JsonFile> FilesQuote { get; set; }
    public List<JsonFile> FilesInvoice { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PaymentLine, PaymentLineVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.PaymentLineId))
               //.ForMember(d => d.PaymentId, opt => opt.MapFrom(s => s.Payment))
               .ForMember(d => d.ExpenseTypeId, opt => opt.MapFrom(s => s.ExpenseType))
               .ForMember(d => d.FilesQuote, opt => opt.MapFrom(s => s.Files.Where(f => f.Type == "quote")))
               .ForMember(d => d.FilesInvoice, opt => opt.MapFrom(s => s.Files.Where(f => f.Type == "invoice")))
               .ForMember(d => d.EmployeeIds, opt => opt.MapFrom(s => s.Employees.Select(o => new OptionDto(o.EmployeeId, o.Employee.FirstName + " " + o.Employee.LastName + " (" + o.Employee.Code + ")"))))
               .ForMember(d => d.EmployeeDivisionIds, opt => opt.MapFrom(s => s.EmployeeDivisions.Select(o => new OptionDto(o.EmployeeDivisionId, o.EmployeeDivision.Name))))
               .ForMember(d => d.CostCenterIds, opt => opt.MapFrom(s => s.CostCenters.Select(o => new OptionDto(o.CostCenterId, o.CostCenter.Name))))
               .ForMember(d => d.CostSubDepartmentIds, opt => opt.MapFrom(s => s.SubDepartments.Select(o => new OptionDto(o.CostSubDepartmentId, o.CostSubDepartment.Name))));
    }
}
