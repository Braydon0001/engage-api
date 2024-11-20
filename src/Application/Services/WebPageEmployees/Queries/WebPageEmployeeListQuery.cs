// auto-generated
namespace Engage.Application.Services.WebPageEmployees.Queries;

public class WebPageEmployeeListQuery : IRequest<List<WebPageEmployeeDto>>
{

}

public class WebPageEmployeeListHandler : ListQueryHandler, IRequestHandler<WebPageEmployeeListQuery, List<WebPageEmployeeDto>>
{
    public WebPageEmployeeListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<WebPageEmployeeDto>> Handle(WebPageEmployeeListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.WebPageEmployees.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.WebPageEmployeeId)
                              .ProjectTo<WebPageEmployeeDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}