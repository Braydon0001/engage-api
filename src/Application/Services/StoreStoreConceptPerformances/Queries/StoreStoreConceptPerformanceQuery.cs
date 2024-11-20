using Engage.Application.Services.StoreStoreConceptPerformances.Models;

namespace Engage.Application.Services.StoreStoreConceptPerformances.Queries;

public class StoreStoreConceptPerformanceQuery : IRequest<ListResult<StoreStoreConceptPerformanceDto>>
{
    public int? StoreId { get; set; }
}

public class StoreStoreConceptPerformanceQueryHandler : BaseQueryHandler, IRequestHandler<StoreStoreConceptPerformanceQuery, ListResult<StoreStoreConceptPerformanceDto>>
{
    public StoreStoreConceptPerformanceQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<StoreStoreConceptPerformanceDto>> Handle(StoreStoreConceptPerformanceQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.StoreStoreConceptPerformances.AsQueryable();

        if (request.StoreId.HasValue)
        {
            queryable = queryable.Where(e => e.StoreId == request.StoreId);
        }

        var entities = await queryable.ProjectTo<StoreStoreConceptPerformanceDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);

        return new ListResult<StoreStoreConceptPerformanceDto>(entities);
    }
}