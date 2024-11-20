// auto-generated
namespace Engage.Application.Services.EmployeeRecurringTransactionStatuses.Queries;

public class EmployeeRecurringTransactionStatusVm : IMapFrom<EmployeeRecurringTransactionStatus>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeRecurringTransactionStatus, EmployeeRecurringTransactionStatusVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeRecurringTransactionStatusId));
    }
}
