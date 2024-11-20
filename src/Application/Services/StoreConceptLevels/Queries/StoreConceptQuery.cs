using Engage.Application.Services.StoreConceptLevels.Models;

namespace Engage.Application.Services.StoreConceptLevels.Queries
{
    public class StoreConceptQuery : IRequest<List<StoreConceptVm>>
    {
    }

    public class StoreConceptQueryHandler : BaseQueryHandler, IRequestHandler<StoreConceptQuery, List<StoreConceptVm>>
    {
        public StoreConceptQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<List<StoreConceptVm>> Handle(StoreConceptQuery request, CancellationToken cancellationToken)
        {
            var queryable = _context.StoreConcepts.AsQueryable();

            var entities = await queryable.ProjectTo<StoreConceptVm>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
            
            var queryableAttribute = _context.StoreConceptAttributes.AsQueryable();

            foreach (var entity in entities)
            {
                var attributes = queryableAttribute.Where(e => e.StoreConceptId == entity.Id).ToList();
                if (attributes.Any())
                {
                    entity.HasAttributes = true;
                }
            }

            return new List<StoreConceptVm>(entities);
        }
    }
}