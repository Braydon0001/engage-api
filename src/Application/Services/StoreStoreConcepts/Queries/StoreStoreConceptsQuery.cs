using Engage.Application.Services.StoreStoreConcepts.Models;

namespace Engage.Application.Services.StoreStoreConcepts.Queries
{
    public class StoreStoreConceptsQuery : IRequest<List<StoreStoreConceptDto>>
    {
        public int? StoreId { get; set; }
    }

    public class StoreStoreConceptsHandler : BaseQueryHandler, IRequestHandler<StoreStoreConceptsQuery, List<StoreStoreConceptDto>>
    {
        public StoreStoreConceptsHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<List<StoreStoreConceptDto>> Handle(StoreStoreConceptsQuery request, CancellationToken cancellationToken)
        {
            var queryable = _context.StoreStoreConcepts.AsQueryable();

            if (request.StoreId.HasValue)
            {
                queryable = queryable.Where(e => e.StoreId == request.StoreId.Value);
            }

            var entities = await queryable.ProjectTo<StoreStoreConceptDto>(_mapper.ConfigurationProvider)
                                          .ToListAsync(cancellationToken);

            return new List<StoreStoreConceptDto>(entities);
        }
    }
}