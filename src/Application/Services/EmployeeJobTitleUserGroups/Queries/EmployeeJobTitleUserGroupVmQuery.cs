using Engage.Application.Services.EmployeeJobTitleUserGroups.Models;

namespace Engage.Application.Services.EmployeeJobTitleUserGroups.Queries;

public class EmployeeJobTitleUserGroupVmQuery : GetByIdQuery, IRequest<EmployeeJobTitleUserGroupVm>
{
}

public class EmployeeJobTitleUserGroupVmQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeJobTitleUserGroupVmQuery, EmployeeJobTitleUserGroupVm>
{
    public EmployeeJobTitleUserGroupVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeJobTitleUserGroupVm> Handle(EmployeeJobTitleUserGroupVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeJobTitleUserGroups.Include(e => e.EmployeeJobTitle)
                                                          .Include(e => e.UserGroup)
                                                          .SingleAsync(e => e.EmployeeJobTitleUserGroupId == request.Id, cancellationToken);

        return _mapper.Map<EmployeeJobTitleUserGroup, EmployeeJobTitleUserGroupVm>(entity);
    }
}
