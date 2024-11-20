using Engage.Application.Services.StoreConceptAttributes.Models;

namespace Engage.Application.Services.StoreConceptAttributes.Queries
{
    public class SingleStoreConceptAttributeQuery : IRequest<List<StoreConceptAttributeDto>>
    {
        public int? ConceptId { get; set; }
    }

    public class SingleStoreConceptAttributeQueryHandler : BaseQueryHandler, IRequestHandler<SingleStoreConceptAttributeQuery, List<StoreConceptAttributeDto>>
    {
        public SingleStoreConceptAttributeQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<List<StoreConceptAttributeDto>> Handle(SingleStoreConceptAttributeQuery request, CancellationToken cancellationToken)
        {
            var queryable = _context.StoreConceptAttributes.AsQueryable();

            if (request.ConceptId.HasValue)
            {
                queryable = queryable.Where(e => e.StoreConceptId == request.ConceptId.Value);
            }

            var entities = await queryable.ProjectTo<StoreConceptAttributeDto>(_mapper.ConfigurationProvider)
                                          .ToListAsync(cancellationToken);

            return new List<StoreConceptAttributeDto>(entities);
        }
    }
}