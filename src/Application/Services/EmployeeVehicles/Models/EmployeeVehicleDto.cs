namespace Engage.Application.Services.EmployeeVehicles.Models;

public class EmployeeVehicleDto : IMapFrom<EmployeeVehicle>
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string EmployeeCode { get; set; }
    public string EmployeeName { get; set; }
    public DateTime? EmployeeEndDate { get; set; }
    public int VehicleTypeId { get; set; }
    public string VehicleTypeName { get; set; }
    public int VehicleBrandId { get; set; }
    public string VehicleBrandName { get; set; }
    public int AssetStatusId { get; set; }
    public string AssetStatusName { get; set; }
    public int AssetOwnerId { get; set; }
    public string AssetOwnerName { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Tracker { get; set; }
    public string Year { get; set; }
    public string RegistrationNumber { get; set; }
    public string Vin { get; set; }
    public string Note { get; set; }
    public bool Disabled { get; set; }
    public DateTime? RecievedDate { get; set; }
    public DateTime? HandedBackDate { get; set; }
    public List<EmployeeVehicleHistoryDto> EmployeeVehicleHistories { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeVehicle, EmployeeVehicleDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeVehicleId))
            .ForMember(d => d.EmployeeName, opt => opt.MapFrom(s => $"{s.Employee.FirstName} {s.Employee.LastName}"))
            .ForMember(d => d.EmployeeVehicleHistories,
                    opt => opt.MapFrom(s => s.EmployeeVehicleHistories
                        .OrderByDescending(e => e.EmployeeVehicleHistoryId)
                        .Select(e => new EmployeeVehicleHistoryDto()
                        {
                            Id = e.EmployeeVehicleHistoryId,
                            EmployeeCode = e.OldEmployee.Code,
                            EmployeeId = e.OldEmployeeId,
                            EmployeeName = $"{e.OldEmployee.FirstName} {e.OldEmployee.LastName}",
                            EmployeeVehicleId = e.EmployeeVehicleId,
                            CreatedDate = e.Created,
                        })
                        .ToList()));
    }
}

public class EmployeeVehicleHistoryDto //: IMapFrom<EmployeeVehicleHistory>
{
    public int Id { get; set; }
    public int EmployeeVehicleId { get; set; }
    public int EmployeeId { get; set; }
    public string EmployeeCode { get; set; }
    public string EmployeeName { get; set; }
    public DateTime? CreatedDate { get; set; }
    //public string Note { get; set; }
    //public DateTime? RecievedDate { get; set; }
    //public DateTime? HandedBackDate { get; set; }

    //public void Mapping(Profile profile)
    //{
    //    profile.CreateMap<EmployeeVehicleHistory, EmployeeVehicleHistoryDto>()
    //        .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeVehicleHistoryId))
    //        .ForMember(d=> d.EmployeeId, opt => opt.MapFrom(s=> s.OldEmployeeId))
    //        .ForMember(d => d.Note, opt => opt.MapFrom(s => s.EmployeeVehicle.Note))
    //        .ForMember(d => d.RecievedDate, opt => opt.MapFrom(s => s.EmployeeVehicle.RecievedDate))
    //        .ForMember(d => d.HandedBackDate, opt => opt.MapFrom(s => s.EmployeeVehicle.HandedBackDate))
    //        .ForMember(d => d.EmployeeName, opt => opt.MapFrom(s => $"{s.OldEmployee.FirstName} {s.OldEmployee.LastName}"));
    //}
}
