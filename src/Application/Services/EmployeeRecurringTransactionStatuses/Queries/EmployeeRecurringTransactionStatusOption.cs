// auto-generated
namespace Engage.Application.Services.EmployeeRecurringTransactionStatuses.Queries;

public class EmployeeRecurringTransactionStatusOption : IMapFrom<EmployeeRecurringTransactionStatus>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeRecurringTransactionStatus, EmployeeRecurringTransactionStatusOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeRecurringTransactionStatusId));
    }
}