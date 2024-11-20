using Engage.Application.Services.StoreConcepts.Models;

namespace Engage.Application.Services.StoreConcepts.Queries;

public class StoreConceptsQuery : GetQuery, IRequest<ListResult<StoreConceptDto>>
{
}

public class StoreConceptsQueryHandler : BaseQueryHandler, IRequestHandler<StoreConceptsQuery, ListResult<StoreConceptDto>>
{
    public StoreConceptsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<StoreConceptDto>> Handle(StoreConceptsQuery request, CancellationToken cancellationToken)
    {

        var entities = await _context.StoreConcepts.OrderBy(e => e.Name)
                                                     .ProjectTo<StoreConceptDto>(_mapper.ConfigurationProvider)
                                                     .ToListAsync(cancellationToken);

        return new ListResult<StoreConceptDto>(entities);
    }
}
