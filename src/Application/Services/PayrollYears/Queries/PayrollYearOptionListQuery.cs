// auto-generated
namespace Engage.Application.Services.PayrollYears.Queries;

public class PayrollYearOptionListQuery : IRequest<List<PayrollYearOption>>
{ 

}

public class PayrollYearOptionListHandler : ListQueryHandler, IRequestHandler<PayrollYearOptionListQuery, List<PayrollYearOption>>
{
    public PayrollYearOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<PayrollYearOption>> Handle(PayrollYearOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.PayrollYears.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<PayrollYearOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}