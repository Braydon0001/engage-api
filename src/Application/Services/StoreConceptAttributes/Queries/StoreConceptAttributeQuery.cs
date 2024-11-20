using Engage.Application.Services.StoreConceptAttributes.Models;

namespace Engage.Application.Services.StoreConceptAttributes.Queries
{
    public class StoreConceptAttributeQuery : IRequest<List<StoreConceptAttributeDto>>
    {
        public int? ConceptId { get; set; }
    }

    public class StoreConceptAttributeQueryHandler : BaseQueryHandler, IRequestHandler<StoreConceptAttributeQuery, List<StoreConceptAttributeDto>>
    {
        public StoreConceptAttributeQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<List<StoreConceptAttributeDto>> Handle(StoreConceptAttributeQuery request, CancellationToken cancellationToken)
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