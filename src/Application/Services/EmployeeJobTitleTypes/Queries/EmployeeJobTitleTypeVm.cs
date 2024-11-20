namespace Engage.Application.Services.EmployeeJobTitleTypes.Queries;

public class EmployeeJobTitleTypeVm : IMapFrom<EmployeeJobTitleType>
{
    public int Id { get; init; }
    public OptionDto EmployeeJobTitleId { get; init; }
    public string Name { get; init; }
    public string Description { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeJobTitleType, EmployeeJobTitleTypeVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeJobTitleTypeId))
               .ForMember(d => d.EmployeeJobTitleId, opt => opt.MapFrom(s => new OptionDto(s.EmployeeJobTitleId, s.EmployeeJobTitle.Name)));
    }
}
