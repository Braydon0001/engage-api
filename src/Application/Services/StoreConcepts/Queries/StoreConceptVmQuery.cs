using Engage.Application.Services.StoreConcepts.Models;

namespace Engage.Application.Services.StoreConcepts.Queries;

public class StoreConceptVmQuery : GetByIdQuery, IRequest<StoreConceptVm>
{
}

public class StoreConceptVMQueryHandler : BaseQueryHandler, IRequestHandler<StoreConceptVmQuery, StoreConceptVm>
{
    public StoreConceptVMQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<StoreConceptVm> Handle(StoreConceptVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.StoreConcepts.Include(e => e.EngageDepartment)
                                                 .SingleAsync(x => x.Id == request.Id, cancellationToken);

        return _mapper.Map<StoreConcept, StoreConceptVm>(entity);
    }
}
