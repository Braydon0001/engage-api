// auto-generated
namespace Engage.Application.Services.EmployeeTransactionStatuses.Queries;

public class EmployeeTransactionStatusOption : IMapFrom<EmployeeTransactionStatus>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeTransactionStatus, EmployeeTransactionStatusOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeTransactionStatusId));
    }
}