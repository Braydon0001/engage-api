// auto-generated
namespace Engage.Application.Services.PayrollYears.Queries;

public class PayrollYearVmQuery : IRequest<PayrollYearVm>
{
    public int Id { get; set; }
}

public class PayrollYearVmHandler : VmQueryHandler, IRequestHandler<PayrollYearVmQuery, PayrollYearVm>
{
    public PayrollYearVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<PayrollYearVm> Handle(PayrollYearVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.PayrollYears.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.PayrollYearId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<PayrollYearVm>(entity);
    }
}