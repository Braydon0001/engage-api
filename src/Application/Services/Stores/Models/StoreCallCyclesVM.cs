using Engage.Application.Services.EmployeeStores.Models;
using Engage.Application.Services.Stores.Queries;

namespace Engage.Application.Services.Stores.Models;

public class StoreCallCyclesVM
{
    public StoreDto Store { get; set; }
    public ListResult<EmployeeStoreDto> EmployeeStores { get; set; }
}
