namespace Engage.Application.Services.EmployeeFuels.Models;

public class EmployeeFuelVm : IMapFrom<EmployeeFuel>
{
    public int Id { get; set; }
    public OptionDto EmployeeId { get; set; }
    public OptionDto EmployeeVehicleId { get; set; }
    public OptionDto EmployeePaymentTypeId { get; set; }
    public OptionDto EmployeeFuelExpenseTypeId { get; set; }
    public string TollgateName { get; set; }
    public DateTime FuelDate { get; set; }
    public decimal Amount { get; set; }
    public float Litres { get; set; }
    public int Odometer { get; set; }
    public string BlobUrl { get; set; }
    public List<FileDto> Files { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeFuel, EmployeeFuelVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeFuelId))
            .ForMember(d => d.EmployeeId, opt => opt.MapFrom(s => new OptionDto(s.EmployeeId, $"{s.Employee.FirstName} {s.Employee.LastName}")))
            .ForMember(d => d.EmployeeVehicleId, opt => opt.MapFrom(s => new OptionDto(s.EmployeeVehicleId, s.EmployeeVehicle.RegistrationNumber)))
            .ForMember(d => d.EmployeePaymentTypeId, opt => opt.MapFrom(s => s.EmployeePaymentTypeId.HasValue
                                                        ? new OptionDto(s.EmployeePaymentTypeId.Value, s.EmployeePaymentType.Name)
                                                        : null))
            .ForMember(d => d.EmployeeFuelExpenseTypeId, opt => opt.MapFrom(s => new OptionDto(s.EmployeeFuelExpenseTypeId, s.EmployeeFuelExpenseType.Name)))
            .ForMember(d => d.Files, opt => opt.MapFrom(s => !string.IsNullOrWhiteSpace(s.BlobUrl) && !string.IsNullOrWhiteSpace(s.BlobName)
                                                        ? new List<FileDto> { new FileDto { Name = s.BlobName, Url = s.BlobUrl } }
                                                        : null));
    }
}
