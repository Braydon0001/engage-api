using Engage.Application.Services.StoreStoreConcepts.Models;

namespace Engage.Application.Services.StoreStoreConcepts.Queries
{
    public class StoreConceptsGroupQuery : IRequest<List<StoreConceptsGroup>>
    {
        public int? StoreId { get; set; }
    }

    public class StoreConceptsGroupHandler : BaseQueryHandler, IRequestHandler<StoreConceptsGroupQuery, List<StoreConceptsGroup>>
    {
        public StoreConceptsGroupHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<List<StoreConceptsGroup>> Handle(StoreConceptsGroupQuery request, CancellationToken cancellationToken)
        {
            var queryable = _context.StoreStoreConcepts.AsQueryable();

            if (request.StoreId.HasValue)
            {
                queryable = queryable.Where(e => e.StoreId == request.StoreId.Value);
            }

            var entities = await queryable.ProjectTo<StoreStoreConceptDto>(_mapper.ConfigurationProvider)
                                          .ToListAsync(cancellationToken);

            var StoreConceptsGroupList = new List<StoreConceptsGroup>();

            StoreConceptsGroupList = entities.GroupBy(e => new { e.StoreId, e.StoreName })
                .Select(e => new StoreConceptsGroup
                { 
                    StoreId = e.Key.StoreId, 
                    StoreName = e.Key.StoreName, 
                    Concepts = entities
                                    .Where(c => c.StoreId == e.Key.StoreId)
                                    .Select(f => new 
                                    { 
                                        ConceptName = f.StoreConceptName, 
                                        Level = f.Level 
                                    }).ToDictionary( t => t.ConceptName, t => t.Level)
                }).ToList();

            return StoreConceptsGroupList;
        }
    }
}