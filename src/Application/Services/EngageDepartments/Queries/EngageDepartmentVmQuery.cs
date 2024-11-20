using Engage.Application.Services.EngageDepartments.Models;

namespace Engage.Application.Services.EngageDepartments.Queries;

public class EngageDepartmentVmQuery : GetByIdQuery, IRequest<EngageDepartmentVm>
{
}

public class EngageDepartmentVmHandler : BaseQueryHandler, IRequestHandler<EngageDepartmentVmQuery, EngageDepartmentVm>
{
    public EngageDepartmentVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EngageDepartmentVm> Handle(EngageDepartmentVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.EngageDepartments.Include(e => e.EngageDepartmentGroup)
                                                     .SingleAsync(x => x.Id == request.Id, cancellationToken);

        return _mapper.Map<EngageDepartment, EngageDepartmentVm>(entity);
    }
}
