// auto-generated
namespace Engage.Application.Services.WebPages.Queries;

public class WebPageOptionListQuery : IRequest<List<WebPageOption>>
{ 

}

public class WebPageOptionListHandler : ListQueryHandler, IRequestHandler<WebPageOptionListQuery, List<WebPageOption>>
{
    public WebPageOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<WebPageOption>> Handle(WebPageOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.WebPages.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.WebPageId)
                              .ProjectTo<WebPageOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}