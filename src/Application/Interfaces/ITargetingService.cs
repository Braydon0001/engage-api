using Engage.Application.Targetings;

namespace Engage.Application.Interfaces;

public interface ITargetingService
{
    Task<string> SerializeEmployeeCriteria(EmployeeTargetingCommand command, CancellationToken cancellationToken);
    Task<string> SerializeStoreCriteria(StoreTargetingCommand command, CancellationToken cancellationToken);
    EmployeeTargetingCriteria DeserializeEmployeeCriteria(string criteria);
    StoreTargetingCriteria DeserializeStoreCriteria(string criteria);
}
