using Engage.Application.Services.EmployeeWorkRoleContacts.Models;

namespace Engage.Application.Services.EmployeeWorkRoleContacts.Queries;

public class EmployeeWorkRoleContactVmQuery : GetByIdQuery, IRequest<EmployeeWorkRoleContactVm>
{
}

public class EmployeeWorkRoleContactVmQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeWorkRoleContactVmQuery, EmployeeWorkRoleContactVm>
{
    public EmployeeWorkRoleContactVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeWorkRoleContactVm> Handle(EmployeeWorkRoleContactVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeWorkRoleContacts.Include(e => e.EmployeeWorkRole)
                                                          .SingleAsync(e => e.EmployeeWorkRoleContactId == request.Id, cancellationToken);

        return _mapper.Map<EmployeeWorkRoleContact, EmployeeWorkRoleContactVm>(entity);
    }
}
