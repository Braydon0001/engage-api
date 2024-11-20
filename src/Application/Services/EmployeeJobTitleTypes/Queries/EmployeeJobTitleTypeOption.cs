namespace Engage.Application.Services.EmployeeJobTitleTypes.Queries;

public class EmployeeJobTitleTypeOption : IMapFrom<EmployeeJobTitleType>
{
    public int Id { get; init; }
    public int ParentId { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeJobTitleType, EmployeeJobTitleTypeOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeJobTitleTypeId))
               .ForMember(d => d.ParentId, opt => opt.MapFrom(s => s.EmployeeJobTitleId));
    }
}