using Engage.Application.Services.WebEvents.Models;

namespace Engage.Application.Services.WebEvents.Queries
{
    public class WebEventVmQuery : IRequest<WebEventVm>
    {
        public int Id { get; set; }
    }

    public class WebEventVmQueryHandler : BaseQueryHandler, IRequestHandler<WebEventVmQuery, WebEventVm>
    {
        public WebEventVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<WebEventVm> Handle(WebEventVmQuery request, CancellationToken cancellationToken)
        {
            var queryable = _context.WebEvents.AsQueryable();

            queryable = queryable.Where(e => e.WebEventId == request.Id);

            var entity = await queryable.ProjectTo<WebEventVm>(_mapper.ConfigurationProvider)
                                          .SingleAsync(cancellationToken);

            return entity;
        }
    }
}