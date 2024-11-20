using Engage.Application.Services.EngageDepartmentGroups.Models;

namespace Engage.Application.Services.EngageDepartmentGroups.Queries;

public class EngageDepartmentGroupVmQuery : GetByIdQuery, IRequest<EngageDepartmentGroupVm>
{
}

public class EngageDepartmentGroupVmHandler : BaseQueryHandler, IRequestHandler<EngageDepartmentGroupVmQuery, EngageDepartmentGroupVm>
{
    public EngageDepartmentGroupVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EngageDepartmentGroupVm> Handle(EngageDepartmentGroupVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.EngageDepartmentGroups.SingleAsync(x => x.Id == request.Id, cancellationToken);

        return _mapper.Map<EngageDepartmentGroup, EngageDepartmentGroupVm>(entity);
    }
}
