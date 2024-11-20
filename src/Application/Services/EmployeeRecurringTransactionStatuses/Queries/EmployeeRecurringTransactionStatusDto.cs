// auto-generated
namespace Engage.Application.Services.EmployeeRecurringTransactionStatuses.Queries;

public class EmployeeRecurringTransactionStatusDto : IMapFrom<EmployeeRecurringTransactionStatus>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeRecurringTransactionStatus, EmployeeRecurringTransactionStatusDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeRecurringTransactionStatusId));
    }
}
