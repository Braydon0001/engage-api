// auto-generated
namespace Engage.Application.Services.PayrollPeriods.Queries;

public class PayrollPeriodVmQuery : IRequest<PayrollPeriodVm>
{
    public int Id { get; set; }
}

public class PayrollPeriodVmHandler : VmQueryHandler, IRequestHandler<PayrollPeriodVmQuery, PayrollPeriodVm>
{
    public PayrollPeriodVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<PayrollPeriodVm> Handle(PayrollPeriodVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.PayrollPeriods.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.PayrollYear);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.PayrollPeriodId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<PayrollPeriodVm>(entity);
    }
}