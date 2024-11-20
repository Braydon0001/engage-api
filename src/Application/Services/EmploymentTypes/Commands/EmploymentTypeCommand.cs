namespace Engage.Application.Services.EmploymentTypes.Commands;

public class EmploymentTypeCommand : IMapTo<EmploymentType>
{
    public string Name { get; set; }
    public int? EndDateReminderDays { get; set; }
    public int Disabled { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmploymentTypeCommand, EmploymentType>();
    }
}
