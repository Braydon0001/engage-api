namespace Engage.Application.Services.EmployeeJobTitles.Commands;

public class EmployeeJobTitleCommand : IMapTo<EmployeeJobTitle>
{
    public int? ParentId { get; set; }
    public int Level { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int? Order { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeJobTitleCommand, EmployeeJobTitle>();
    }
}
