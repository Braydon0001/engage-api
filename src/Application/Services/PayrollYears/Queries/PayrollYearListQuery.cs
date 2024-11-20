// auto-generated
namespace Engage.Application.Services.PayrollYears.Queries;

public class PayrollYearListQuery : IRequest<List<PayrollYearDto>>
{

}

public class PayrollYearListHandler : ListQueryHandler, IRequestHandler<PayrollYearListQuery, List<PayrollYearDto>>
{
    public PayrollYearListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<PayrollYearDto>> Handle(PayrollYearListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.PayrollYears.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<PayrollYearDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}