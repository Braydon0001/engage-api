using Engage.Application.Services.EmployeeSuspensions.Models;

namespace Engage.Application.Services.EmployeeSuspensions.Queries;

public class EmployeeSuspensionVmQuery : GetByIdQuery, IRequest<EmployeeSuspensionVm>
{
}

public class EmployeeSuspensionVmQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeSuspensionVmQuery, EmployeeSuspensionVm>
{
    public EmployeeSuspensionVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeSuspensionVm> Handle(EmployeeSuspensionVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeSuspensions.Include(e => e.Employee)
                                                          .Include(e => e.EmployeeSuspensionReason)
                                                          .SingleAsync(e => e.EmployeeSuspensionId == request.Id, cancellationToken);

        return _mapper.Map<EmployeeSuspension, EmployeeSuspensionVm>(entity);
    }
}
