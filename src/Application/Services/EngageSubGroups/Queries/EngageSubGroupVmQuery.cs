using Engage.Application.Services.EngageSubGroups.Models;

namespace Engage.Application.Services.EngageSubGroups.Queries;

public class EngageSubGroupVmQuery : GetByIdQuery, IRequest<EngageSubGroupVm>
{
}

public class EngageSubGroupVMQueryHandler : BaseQueryHandler, IRequestHandler<EngageSubGroupVmQuery, EngageSubGroupVm>
{
    public EngageSubGroupVMQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EngageSubGroupVm> Handle(EngageSubGroupVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.EngageSubGroups.Include(e => e.EngageGroup)
                                                   .Include(e => e.StoreDepartment)
                                                   .Include(e => e.EngageDepartment)
                                                   .Include(e => e.EngageSubGroupSuppliers)
                                                   .ThenInclude(e => e.Supplier)
                                                   .SingleAsync(x => x.Id == request.Id, cancellationToken);

        return _mapper.Map<EngageSubGroup, EngageSubGroupVm>(entity);
    }
}
