namespace Engage.Application.Services.EmployeeFuels.Models;

public class EmployeeFuelDto : IMapFrom<EmployeeFuel>
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; }
    public int EmployeeVehicleId { get; set; }
    public string EmployeeVehicleRegistrationNumber { get; set; }
    public int? EmployeePaymentTypeId { get; set; }
    public string EmployeePaymentTypeName { get; set; }
    public int EmployeeFuelExpenseTypeId { get; set; }
    public string EmployeeFuelExpenseTypeName { get; set; }
    public string TollgateName { get; set; }
    public DateTime FuelDate { get; set; }
    public decimal? Amount { get; set; }
    public float? Litres { get; set; }
    public int? Odometer { get; set; }
    public string BlobUrl { get; set; }

    public List<FileDto> Files { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeFuel, EmployeeFuelDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeFuelId))
            .ForMember(d => d.EmployeeName, opt => opt.MapFrom(s => $"{s.Employee.FirstName} {s.Employee.LastName}"))
            .ForMember(d => d.Files, opt => opt.MapFrom(s => !string.IsNullOrWhiteSpace(s.BlobUrl) && !string.IsNullOrWhiteSpace(s.BlobName)
                                                        ? new List<FileDto> { new FileDto { Name = s.BlobName, Url = s.BlobUrl } }
                                                        : null));
    }
}
