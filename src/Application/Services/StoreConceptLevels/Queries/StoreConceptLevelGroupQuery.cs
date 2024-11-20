using Engage.Application.Services.StoreConceptLevels.Models;

namespace Engage.Application.Services.StoreConceptLevels.Queries
{
    public class StoreConceptLevelGroupQuery : IRequest<List<StoreConceptLevelGroup>>
    {
        public int? StoreId { get; set; }
    }

    public class StoreConceptLevelGroupQueryHandler : BaseQueryHandler, IRequestHandler<StoreConceptLevelGroupQuery, List<StoreConceptLevelGroup>>
    {
        public StoreConceptLevelGroupQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<List<StoreConceptLevelGroup>> Handle(StoreConceptLevelGroupQuery request, CancellationToken cancellationToken)
        {
            var queryable = _context.StoreConceptLevels.AsQueryable();

            if (request.StoreId.HasValue)
            {
                queryable = queryable.Where(e => e.StoreId == request.StoreId.Value);
            }

            var entities = await queryable.ProjectTo<StoreConceptLevelDto>(_mapper.ConfigurationProvider)
                                          .ToListAsync(cancellationToken);

            var StoreConceptsGroupList = new List<StoreConceptLevelGroup>();

            StoreConceptsGroupList = entities.GroupBy(e => new { e.StoreId, e.StoreName })
                .Select(e => new StoreConceptLevelGroup
                {
                    StoreId = e.Key.StoreId,
                    StoreName = e.Key.StoreName,
                    Concepts = entities
                                    .Where(c => c.StoreId == e.Key.StoreId)
                                    .Select(f => new
                                    {
                                        Name = f.StoreConceptName,
                                        Level = f.Level,
                                        Id = f.Id,
                                    })
                }).ToList();

            return StoreConceptsGroupList;
        }
    }
}