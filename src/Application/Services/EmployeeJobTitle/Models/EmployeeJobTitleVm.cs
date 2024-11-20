namespace Engage.Application.Services.EmployeeJobTitles.Models;

public class EmployeeJobTitleVm : IMapFrom<EmployeeJobTitle>
{
    public int Id { get; set; }
    public OptionDto ParentId { get; set; }
    public int Level { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int? Order { get; set; }
    public bool Disabled { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeJobTitle, EmployeeJobTitleVm>()
              .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeJobTitleId))
              .ForMember(d => d.ParentId, opt => opt.MapFrom(s => s.ParentId.HasValue ? new OptionDto(s.ParentId.Value, s.Parent.Name) : null));
    }
}
