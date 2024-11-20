namespace Engage.Application.Services.EmployeeJobTitles.Queries;

public class EmployeeJobTitleDto : IMapFrom<EmployeeJobTitle>
{
    public int Id { get; set; }
    public int? ParentId { get; set; }
    public string ParentName { get; set; }
    public int Level { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int? Order { get; set; }
    public bool Disabled { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeJobTitle, EmployeeJobTitleDto>()
             .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeJobTitleId))
             .ForMember(d => d.ParentName, opt => opt.MapFrom(s => s.ParentId.HasValue ? s.Parent.Name : null));
    }
}
