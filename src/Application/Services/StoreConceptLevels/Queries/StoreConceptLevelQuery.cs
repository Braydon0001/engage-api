using Engage.Application.Services.StoreConceptLevels.Models;

namespace Engage.Application.Services.StoreConceptLevels.Queries
{
    public class StoreConceptLevelQuery : IRequest<List<StoreConceptLevelDto>>
    {
        public int? StoreId { get; set; }
    }

    public class StoreStoreConceptsHandler : BaseQueryHandler, IRequestHandler<StoreConceptLevelQuery, List<StoreConceptLevelDto>>
    {
        public StoreStoreConceptsHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<List<StoreConceptLevelDto>> Handle(StoreConceptLevelQuery request, CancellationToken cancellationToken)
        {
            var queryable = _context.StoreConceptLevels.AsQueryable();

            if (request.StoreId.HasValue)
            {
                queryable = queryable.Where(e => e.StoreId == request.StoreId.Value);
            }

            var entities = await queryable.ProjectTo<StoreConceptLevelDto>(_mapper.ConfigurationProvider)
                                          .ToListAsync(cancellationToken);

            return new List<StoreConceptLevelDto>(entities);
        }
    }
}