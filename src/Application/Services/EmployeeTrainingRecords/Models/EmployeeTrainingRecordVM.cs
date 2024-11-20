namespace Engage.Application.Services.EmployeeTrainingRecords.Models;

public class EmployeeTrainingRecordVm : IMapFrom<EmployeeTrainingRecord>
{
    public int Id { get; set; }
    public OptionDto EmployeeId { get; set; }
    public OptionDto EmployeeTrainingStatusId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string CourseName { get; set; }
    public string CertificateNo { get; set; }
    public DateTime? CertificateExpiryDate { get; set; }
    public bool IsAddReminder { get; set; }
    public string CourseResult { get; set; }
    public decimal DirectCourseCost { get; set; }
    public string InvoiceNo { get; set; }
    public decimal EmployeeRate { get; set; }
    public string Facilitator { get; set; }
    public decimal TravelCost { get; set; }
    public string Assessor { get; set; }
    public decimal AccommodationCost { get; set; }
    public decimal FacilitatorCost { get; set; }
    public decimal FoodAndBeverageCost { get; set; }
    public decimal Additional5Cost { get; set; }
    public decimal Additional6Cost { get; set; }
    public string Note { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeTrainingRecord, EmployeeTrainingRecordVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeTrainingRecordId))
            .ForMember(d => d.EmployeeId, opt => opt.MapFrom(s => new OptionDto(s.EmployeeId, s.Employee.FirstName + " " + s.Employee.LastName)))
            .ForMember(d => d.EmployeeTrainingStatusId, opt => opt.MapFrom(s => new OptionDto(s.EmployeeTrainingStatusId, s.EmployeeTrainingStatus.Name)));
    }
}
