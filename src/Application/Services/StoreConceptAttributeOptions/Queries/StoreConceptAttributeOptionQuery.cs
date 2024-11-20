using Engage.Application.Services.StoreConceptAttributeOptions.Models;

namespace Engage.Application.Services.StoreConceptAttributeOptions.Queries
{
    public class StoreConceptAttributeOptionQuery : IRequest<List<StoreConceptAttributeOptionDto>>
    {
        public int? StoreConceptAttributeId { get; set; }
    }

    public class StoreConceptAttributeOptionQueryHandler : BaseQueryHandler, IRequestHandler<StoreConceptAttributeOptionQuery, List<StoreConceptAttributeOptionDto>>
    {
        public StoreConceptAttributeOptionQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<List<StoreConceptAttributeOptionDto>> Handle(StoreConceptAttributeOptionQuery request, CancellationToken cancellationToken)
        {
            var queryable = _context.StoreConceptAttributeOptions.AsQueryable();

            if (request.StoreConceptAttributeId.HasValue)
            {
                queryable = queryable.Where(e => e.StoreConceptAttributeId == request.StoreConceptAttributeId.Value);
            }

            var entities = await queryable.ProjectTo<StoreConceptAttributeOptionDto>(_mapper.ConfigurationProvider)
                                          .ToListAsync(cancellationToken);

            return new List<StoreConceptAttributeOptionDto>(entities);
        }
    }
}