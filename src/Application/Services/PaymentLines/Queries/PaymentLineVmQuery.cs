namespace Engage.Application.Services.PaymentLines.Queries;

public record PaymentLineVmQuery(int Id) : IRequest<PaymentLineVm>;

public record PaymentLineVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<PaymentLineVmQuery, PaymentLineVm>
{
    public async Task<PaymentLineVm> Handle(PaymentLineVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.PaymentLines.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.Payment)
                             .Include(e => e.ExpenseType)
                             .Include(e => e.Employees)
                                .ThenInclude(e => e.Employee)
                             .Include(e => e.EmployeeDivisions)
                                .ThenInclude(e => e.EmployeeDivision)
                             .Include(e => e.CostCenters)
                                .ThenInclude(e => e.CostCenter)
                             .Include(e => e.SubDepartments)
                                .ThenInclude(e => e.CostSubDepartment);

        var entity = await queryable.SingleOrDefaultAsync(e => e.PaymentLineId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<PaymentLineVm>(entity);
    }
}