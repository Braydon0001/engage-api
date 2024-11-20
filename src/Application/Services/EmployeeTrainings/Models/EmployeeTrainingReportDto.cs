namespace Engage.Application.Services.EmployeeTrainings.Models;

public class EmployeeTrainingReportDto : IMapFrom<EmployeeTraining>
{
    public string EmpNo { get; set; }
    public string EmployeeName { get; set; }
    public string IdNo { get; set; }
    public string Race { get; set; }
    public string Gender { get; set; }
    public string Disabled { get; set; }
    public string TrainingCourse { get; set; }
    public string InternalTraining { get; set; }
    public string Facilitators { get; set; }
    public string TrainerServiceProvider { get; set; }
    public string Category { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public string Duration { get; set; }
    public string NoOfParticipants { get; set; }
    public string SiteLocation { get; set; }
    public decimal DirectCost { get; set; }
    public decimal AdditionalCost { get; set; }
    public decimal AccommodationCost { get; set; }
    public decimal CarHireCost { get; set; }
    public decimal CateringCost { get; set; }
    public decimal FlightsCost { get; set; }
    public decimal FuelCost { get; set; }
    public decimal StationeryCost { get; set; }
    public decimal VenueCost { get; set; }
    public decimal OtherCost { get; set; }
    public decimal TotalCost { get; set; }
    public string Attachments { get; set; }
    public string Comment { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeTraining, EmployeeTrainingReportDto>()
             .ForMember(d => d.EmployeeName, opt => opt.MapFrom(s => s.Employee.FirstName + " " + s.Employee.LastName))
             .ForMember(d => d.EmpNo, opt => opt.MapFrom(s => s.Employee.Code))
             .ForMember(d => d.IdNo, opt => opt.MapFrom(s => s.Employee.IdNumber))
             .ForMember(d => d.Race, opt => opt.MapFrom(s => s.Employee.EmployeeRace.Name))
             .ForMember(d => d.Gender, opt => opt.MapFrom(s => s.Employee.EmployeeGender.Name))
             .ForMember(d => d.TrainingCourse, opt => opt.MapFrom(s => s.Training.Name))
             .ForMember(d => d.TrainerServiceProvider, opt => opt.MapFrom(s => s.Training.TrainingProvider.Name))
             .ForMember(d => d.Duration, opt => opt.MapFrom(s => s.Training.Duration))
             .ForMember(d => d.NoOfParticipants, opt => opt.MapFrom(s => s.Training.NoOfParticipants))
             .ForMember(d => d.InternalTraining, opt => opt.MapFrom(s => s.Training.IsInternalTraining ? "YES" : "NO"))
             .ForMember(d => d.Facilitators, opt => opt.MapFrom(s => string.Join(", ", s.Training.TrainingFacilitators.Select(s => s.Employee.FirstName + " " + s.Employee.LastName))))
             .ForMember(d => d.Category, opt => opt.MapFrom(s => s.Training.TrainingCategory.Name))
             .ForMember(d => d.StartDate, opt => opt.MapFrom(s => s.Training.StartDate.ToShortDateString()))
             .ForMember(d => d.EndDate, opt => opt.MapFrom(s => s.Training.EndDate.ToShortDateString()))
             .ForMember(d => d.SiteLocation, opt => opt.MapFrom(s => s.Training.Site))
             .ForMember(d => d.DirectCost, opt => opt.MapFrom(s => Math.Round(s.DirectCost, 2)))
             .ForMember(d => d.AdditionalCost, opt => opt.MapFrom(s => Math.Round(s.AdditionalCost, 2)))
             .ForMember(d => d.TotalCost, opt => opt.MapFrom(s => Math.Round(s.TotalCost, 2)))
             .ForMember(d => d.AccommodationCost, opts => opts.MapFrom(s => Math.Round(s.AccommodationCost, 2)))
             .ForMember(d => d.CarHireCost, opts => opts.MapFrom(s => Math.Round(s.CarHireCost, 2)))
             .ForMember(d => d.CateringCost, opts => opts.MapFrom(s => Math.Round(s.CateringCost, 2)))
             .ForMember(d => d.FlightsCost, opts => opts.MapFrom(s => Math.Round(s.FlightsCost, 2)))
             .ForMember(d => d.FuelCost, opts => opts.MapFrom(s => Math.Round(s.FuelCost, 2)))
             .ForMember(d => d.StationeryCost, opts => opts.MapFrom(s => Math.Round(s.StationeryCost, 2)))
             .ForMember(d => d.VenueCost, opts => opts.MapFrom(s => Math.Round(s.VenueCost, 2)))
             .ForMember(d => d.OtherCost, opts => opts.MapFrom(s => Math.Round(s.OtherCost, 2)))
             .ForMember(d => d.Attachments, opt => opt.MapFrom(s => s.Training.Files.Any() ? "YES" : "NO"))
             .ForMember(d => d.Comment, opt => opt.MapFrom(s => s.Training.Note));
    }
}
