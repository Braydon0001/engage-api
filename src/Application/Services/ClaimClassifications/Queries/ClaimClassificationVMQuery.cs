using Engage.Application.Services.ClaimClassifications.Models;

namespace Engage.Application.Services.ClaimClassifications.Queries;

public class ClaimClassificationVmQuery : GetByIdQuery, IRequest<ClaimClassificationVm>
{
}

public class ClaimClassificationVMQueryHandler : BaseQueryHandler, IRequestHandler<ClaimClassificationVmQuery, ClaimClassificationVm>
{
    public ClaimClassificationVMQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ClaimClassificationVm> Handle(ClaimClassificationVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.ClaimClassifications.Include(e => e.ClaimType)
                                                        .ThenInclude(e => e.Vat)
                                                        .Include(e => e.Supplier)
                                                        .Include(e => e.Supplier)
                                                        .Include(e => e.ClaimClassificationTypes)
                                                        .ThenInclude(e=> e.ClaimType)
                                                        .SingleAsync(x => x.ClaimClassificationId == request.Id, cancellationToken);

        return _mapper.Map<ClaimClassification, ClaimClassificationVm>(entity);
    }
}
