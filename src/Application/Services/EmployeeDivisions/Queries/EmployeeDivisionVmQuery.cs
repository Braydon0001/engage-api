namespace Engage.Application.Services.EmployeeDivisions.Queries;

public class EmployeeDivisionVmQuery : IRequest<EmployeeDivisionVm>
{
    public int Id { get; set; }
}

public class EmployeeDivisionVmHandler : VmQueryHandler, IRequestHandler<EmployeeDivisionVmQuery, EmployeeDivisionVm>
{
    public EmployeeDivisionVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeDivisionVm> Handle(EmployeeDivisionVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeeDivisions.AsQueryable().AsNoTracking();

        var entity = await queryable.SingleOrDefaultAsync(e => e.EmployeeDivisionId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<EmployeeDivisionVm>(entity);
    }
}