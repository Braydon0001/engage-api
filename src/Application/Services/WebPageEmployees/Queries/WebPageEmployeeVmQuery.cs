// auto-generated
namespace Engage.Application.Services.WebPageEmployees.Queries;

public class WebPageEmployeeVmQuery : IRequest<WebPageEmployeeVm>
{
    public int Id { get; set; }
}

public class WebPageEmployeeVmHandler : VmQueryHandler, IRequestHandler<WebPageEmployeeVmQuery, WebPageEmployeeVm>
{
    public WebPageEmployeeVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<WebPageEmployeeVm> Handle(WebPageEmployeeVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.WebPageEmployees.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.Employee)
                             .Include(e => e.WebPage);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.WebPageEmployeeId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<WebPageEmployeeVm>(entity);
    }
}