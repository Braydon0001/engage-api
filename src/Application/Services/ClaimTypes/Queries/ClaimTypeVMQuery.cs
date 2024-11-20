using Engage.Application.Services.ClaimTypes.Models;

namespace Engage.Application.Services.ClaimTypes.Queries;

public class ClaimTypeVmQuery : GetByIdQuery, IRequest<ClaimTypeVm>
{
}

public class ClaimTypeVMQueryHandler : BaseQueryHandler, IRequestHandler<ClaimTypeVmQuery, ClaimTypeVm>
{
    public ClaimTypeVMQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ClaimTypeVm> Handle(ClaimTypeVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.ClaimTypes.Include(e => e.Vat)
                                              .Include(e => e.Supplier)
                                              .SingleAsync(x => x.ClaimTypeId == request.Id, cancellationToken);

        return _mapper.Map<ClaimType, ClaimTypeVm>(entity);
    }
}
