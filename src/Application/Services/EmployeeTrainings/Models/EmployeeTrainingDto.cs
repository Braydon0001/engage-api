namespace Engage.Application.Services.EmployeeTrainings.Models;

public class EmployeeTrainingDto : IMapFrom<EmployeeTraining>
{
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; }
    public string EmployeeCode { get; set; }
    public int TrainingId { get; set; }
    public string TrainingName { get; set; }
    public decimal DirectCost { get; set; }
    public decimal AdditionalCost { get; set; }
    public decimal TotalCost { get; set; }

    public decimal AccommodationCost { get; set; }
    public decimal CarHireCost { get; set; }
    public decimal CateringCost { get; set; }
    public decimal FlightsCost { get; set; }
    public decimal FuelCost { get; set; }
    public decimal StationeryCost { get; set; }
    public decimal VenueCost { get; set; }
    public decimal OtherCost { get; set; }

    public string Note { get; set; }
    public bool Disabled { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeTraining, EmployeeTrainingDto>()
             .ForMember(d => d.EmployeeName, opts => opts.MapFrom(s => s.Employee.FirstName + " " + s.Employee.LastName))
             .ForMember(d => d.EmployeeCode, opts => opts.MapFrom(s => s.Employee.Code))
             .ForMember(d => d.TrainingName, opts => opts.MapFrom(s => s.Training.Name))
             .ForMember(d => d.DirectCost, opts => opts.MapFrom(s => Math.Round(s.DirectCost, 2)))
             .ForMember(d => d.AdditionalCost, opts => opts.MapFrom(s => Math.Round(s.AdditionalCost, 2)))
             .ForMember(d => d.TotalCost, opts => opts.MapFrom(s => Math.Round(s.TotalCost, 2)))

             .ForMember(d => d.AccommodationCost, opts => opts.MapFrom(s => Math.Round(s.AccommodationCost, 2)))
             .ForMember(d => d.CarHireCost, opts => opts.MapFrom(s => Math.Round(s.CarHireCost, 2)))
             .ForMember(d => d.CateringCost, opts => opts.MapFrom(s => Math.Round(s.CateringCost, 2)))
             .ForMember(d => d.FlightsCost, opts => opts.MapFrom(s => Math.Round(s.FlightsCost, 2)))
             .ForMember(d => d.FuelCost, opts => opts.MapFrom(s => Math.Round(s.FuelCost, 2)))
             .ForMember(d => d.StationeryCost, opts => opts.MapFrom(s => Math.Round(s.StationeryCost, 2)))
             .ForMember(d => d.VenueCost, opts => opts.MapFrom(s => Math.Round(s.VenueCost, 2)))
             .ForMember(d => d.OtherCost, opts => opts.MapFrom(s => Math.Round(s.OtherCost, 2)));
    }
}
