namespace Engage.Application.Services.EmployeeRegionContacts.Commands;

public class EmployeeRegionContactCommand : IMapTo<EmployeeRegionContact>
{
    public int EmployeeId { get; set; }
    public string MobilePhone { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeRegionContactCommand, EmployeeRegionContact>();
    }
}
