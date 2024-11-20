namespace Engage.Application.Services.EmployeeJobTitleTypes.Queries;

public class EmployeeJobTitleTypeDto : IMapFrom<EmployeeJobTitleType>
{
    public int Id { get; init; }
    public int EmployeeJobTitleId { get; init; }
    public string EmployeeJobTitleName { get; init; }
    public string Name { get; init; }
    public string Description { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeJobTitleType, EmployeeJobTitleTypeDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeJobTitleTypeId))
               .ForMember(d => d.EmployeeJobTitleName, opt => opt.MapFrom(s => s.EmployeeJobTitle.Name));
    }
}
