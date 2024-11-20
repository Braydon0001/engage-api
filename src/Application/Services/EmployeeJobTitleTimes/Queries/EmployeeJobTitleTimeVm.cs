namespace Engage.Application.Services.EmployeeJobTitleTimes.Queries;

public class EmployeeJobTitleTimeVm : IMapFrom<EmployeeJobTitleTime>
{
    public int Id { get; init; }
    public OptionDto EmployeeJobTitleId { get; init; }
    public string Name { get; init; }
    public string Description { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeJobTitleTime, EmployeeJobTitleTimeVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeJobTitleTimeId))
               .ForMember(d => d.EmployeeJobTitleId, opt => opt.MapFrom(s => new OptionDto(s.EmployeeJobTitleId, s.EmployeeJobTitle.Name)));
    }
}
