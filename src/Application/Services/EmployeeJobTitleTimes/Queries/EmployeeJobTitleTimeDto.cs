namespace Engage.Application.Services.EmployeeJobTitleTimes.Queries;

public class EmployeeJobTitleTimeDto : IMapFrom<EmployeeJobTitleTime>
{
    public int Id { get; init; }
    public int EmployeeJobTitleId { get; init; }
    public string EmployeeJobTitleName { get; init; }
    public string Name { get; init; }
    public string Description { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeJobTitleTime, EmployeeJobTitleTimeDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeJobTitleTimeId))
               .ForMember(d => d.EmployeeJobTitleName, opt => opt.MapFrom(s => s.EmployeeJobTitle.Name));
    }
}
