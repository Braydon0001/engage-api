namespace Engage.Application.Services.DCDepartments.Commands;

public class DCDepartmentCommand : IMapTo<DCDepartment>
{
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<DCDepartmentCommand, DCDepartment>();
    }
}
