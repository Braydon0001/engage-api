namespace Engage.Application.Services.EmployeePensions.Queries;

public class EmployeePensionVmQuery : IRequest<EmployeePensionVm>
{
    public int Id { get; set; }
}

public class EmployeePensionVmHandler : VmQueryHandler, IRequestHandler<EmployeePensionVmQuery, EmployeePensionVm>
{
    public EmployeePensionVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeePensionVm> Handle(EmployeePensionVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeePensions.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.Employee)
                             .Include(e => e.EmployeePensionScheme)
                             .Include(e => e.EmployeePensionCategory)
                             .Include(e => e.EmployeePensionContributionPercentage);

        var entity = await queryable.SingleOrDefaultAsync(e => e.EmployeePensionId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<EmployeePensionVm>(entity);
    }
}