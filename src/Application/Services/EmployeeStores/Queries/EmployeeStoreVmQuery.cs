using Engage.Application.Services.EmployeeStores.Models;

namespace Engage.Application.Services.EmployeeStores.Queries;

public class EmployeeStoreVmQuery : GetByIdQuery, IRequest<EmployeeStoreVm>
{
}


public class EmployeeStoreVmQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeStoreVmQuery, EmployeeStoreVm>
{
    public EmployeeStoreVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeStoreVm> Handle(EmployeeStoreVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeStores.Include(x => x.Employee)
                                                  .Include(x => x.Store)
                                                  .Include(x => x.EngageSubGroup)
                                                  .ThenInclude(x => x.EngageDepartment)
                                                  .Include(x => x.GetFrequencyType)
                                                  .SingleAsync(x => x.EmployeeStoreId == request.Id, cancellationToken);

        return _mapper.Map<EmployeeStore, EmployeeStoreVm>(entity);
    }
}
