using Engage.Application.Services.DCAccounts.Models;

namespace Engage.Application.Services.DCAccounts.Queries;

public class DCAccountVmQuery : GetByNullableIdQuery, IRequest<DCAccountVm>
{
}

public class DCAccountVmQueryHandler : BaseQueryHandler, IRequestHandler<DCAccountVmQuery, DCAccountVm>
{
    public DCAccountVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<DCAccountVm> Handle(DCAccountVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.DCAccounts.Include(e => e.Store)
                                              .Include(e => e.DistributionCenter)
                                              .SingleAsync(e => e.DCAccountId == request.Id, cancellationToken);

        return _mapper.Map<DCAccount, DCAccountVm>(entity);
    }
}
