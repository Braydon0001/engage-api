namespace Engage.Application.Services.EmployeeVehicles.Models;

public class EmployeeVehicleVm : IMapFrom<EmployeeVehicle>
{
    public int Id { get; set; }
    public OptionDto EmployeeId { get; set; }
    public OptionDto VehicleTypeId { get; set; }
    public OptionDto VehicleBrandId { get; set; }
    public OptionDto AssetStatusId { get; set; }
    public OptionDto AssetOwnerId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Tracker { get; set; }
    public string Year { get; set; }
    public string RegistrationNumber { get; set; }
    public string Vin { get; set; }
    public string Note { get; set; }
    public DateTime? RecievedDate { get; set; }
    public DateTime? HandedBackDate { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeVehicle, EmployeeVehicleVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeVehicleId))
            .ForMember(d => d.EmployeeId, opt => opt.MapFrom(s => new OptionDto(s.EmployeeId, $"{s.Employee.FirstName} {s.Employee.LastName}")))
            .ForMember(d => d.VehicleTypeId, opt => opt.MapFrom(s => new OptionDto(s.VehicleTypeId, s.VehicleType.Name)))
            .ForMember(d => d.VehicleBrandId, opt => opt.MapFrom(s => new OptionDto(s.VehicleBrandId, s.VehicleBrand.Name)))
            .ForMember(d => d.AssetStatusId, opt => opt.MapFrom(s => new OptionDto(s.AssetStatusId, s.AssetStatus.Name)))
            .ForMember(d => d.AssetOwnerId, opt => opt.MapFrom(s => new OptionDto(s.AssetOwnerId, s.AssetOwner.Name)));
    }
}
