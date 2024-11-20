// auto-generated
namespace Engage.Application.Services.WebPages.Queries;

public class WebPageListQuery : IRequest<List<WebPageDto>>
{

}

public class WebPageListHandler : ListQueryHandler, IRequestHandler<WebPageListQuery, List<WebPageDto>>
{
    public WebPageListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<WebPageDto>> Handle(WebPageListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.WebPages.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.WebPageId)
                              .ProjectTo<WebPageDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}