// auto-generated
namespace Engage.Application.Services.WebPageEmployees.Queries;

public class WebPageEmployeeOptionListQuery : IRequest<List<WebPageEmployeeOption>>
{ 

}

public class WebPageEmployeeOptionListHandler : ListQueryHandler, IRequestHandler<WebPageEmployeeOptionListQuery, List<WebPageEmployeeOption>>
{
    public WebPageEmployeeOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<WebPageEmployeeOption>> Handle(WebPageEmployeeOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.WebPageEmployees.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.WebPageEmployeeId)
                              .ProjectTo<WebPageEmployeeOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}