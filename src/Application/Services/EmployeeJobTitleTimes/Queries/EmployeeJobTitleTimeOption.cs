namespace Engage.Application.Services.EmployeeJobTitleTimes.Queries;

public class EmployeeJobTitleTimeOption : IMapFrom<EmployeeJobTitleTime>
{
    public int Id { get; init; }
    public int ParentId { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeJobTitleTime, EmployeeJobTitleTimeOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeJobTitleTimeId))
               .ForMember(d => d.ParentId, opt => opt.MapFrom(s => s.EmployeeJobTitleId));
    }
}