using Engage.Application.Services.EmployeeWorkRoles.Models;

namespace Engage.Application.Services.EmployeeWorkRoles.Queries;

public class EmployeeWorkRoleVmQuery : GetByIdQuery, IRequest<EmployeeWorkRoleVm>
{
}

public class EmployeeWorkRoleVmQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeWorkRoleVmQuery, EmployeeWorkRoleVm>
{
    public EmployeeWorkRoleVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeWorkRoleVm> Handle(EmployeeWorkRoleVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeWorkRoles.Include(e => e.Employee)
                                                     .Include(e => e.Manager)
                                                     .Include(e => e.Status)
                                                     .Include(e => e.EmploymentType)
                                                     .Include(e => e.Grade)
                                                     .SingleAsync(e => e.EmployeeWorkRoleId == request.Id, cancellationToken);

        return _mapper.Map<EmployeeWorkRole, EmployeeWorkRoleVm>(entity);
    }
}
