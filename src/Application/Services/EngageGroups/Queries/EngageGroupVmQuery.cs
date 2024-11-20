using Engage.Application.Services.EngageGroups.Models;

namespace Engage.Application.Services.EngageGroups.Queries;

public class EngageGroupVmQuery : GetByIdQuery, IRequest<EngageGroupVm>
{
}

public class EngageGroupVMQueryHandler : BaseQueryHandler, IRequestHandler<EngageGroupVmQuery, EngageGroupVm>
{
    public EngageGroupVMQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EngageGroupVm> Handle(EngageGroupVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.EngageGroups.SingleAsync(x => x.Id == request.Id, cancellationToken);

        return _mapper.Map<EngageGroup, EngageGroupVm>(entity);
    }
}
