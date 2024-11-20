// auto-generated
namespace Engage.Application.Services.WebPages.Queries;

public class WebPageVmQuery : IRequest<WebPageVm>
{
    public int Id { get; set; }
}

public class WebPageVmHandler : VmQueryHandler, IRequestHandler<WebPageVmQuery, WebPageVm>
{
    public WebPageVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<WebPageVm> Handle(WebPageVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.WebPages.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.WebPageId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<WebPageVm>(entity);
    }
}