using Engage.Application.Services.WebEvents.Models;

namespace Engage.Application.Services.WebEvents.Queries
{
    public class WebEventActiveQuery : IRequest<List<WebEventDto>>
    {
    }

    public class WebEventActiveQueryHandler : BaseQueryHandler, IRequestHandler<WebEventActiveQuery, List<WebEventDto>>
    {
        public WebEventActiveQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<List<WebEventDto>> Handle(WebEventActiveQuery request, CancellationToken cancellationToken)
        {
            var queryable = _context.WebEvents.AsQueryable();

            queryable = queryable.Where(e => e.EndDate >= DateTime.Now);

            var entities = await queryable.ProjectTo<WebEventDto>(_mapper.ConfigurationProvider)
                                          .ToListAsync(cancellationToken);

            return new List<WebEventDto>(entities);
        }
    }
}