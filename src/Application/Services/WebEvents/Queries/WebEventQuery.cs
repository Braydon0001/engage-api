using Engage.Application.Services.WebEvents.Models;

namespace Engage.Application.Services.WebEvents.Queries
{
    public class WebEventQuery : IRequest<List<WebEventDto>>
    {
        public int? Id { get; set; }
    }

    public class WebEventQueryHandler : BaseQueryHandler, IRequestHandler<WebEventQuery, List<WebEventDto>>
    {
        public WebEventQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<List<WebEventDto>> Handle(WebEventQuery request, CancellationToken cancellationToken)
        {
            var queryable = _context.WebEvents.AsQueryable();

            if (request.Id.HasValue)
            {
                queryable = queryable.Where(e => e.WebEventId == request.Id.Value);
            }

            var entities = await queryable.ProjectTo<WebEventDto>(_mapper.ConfigurationProvider)
                                          .ToListAsync(cancellationToken);

            return new List<WebEventDto>(entities);
        }
    }
}