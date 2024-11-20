namespace Engage.Application.Services.EmployeeHealthConditions.Queries;

public class EmployeeHealthConditionVmQuery : IRequest<EmployeeHealthConditionVm>
{
    public int Id { get; set; }
}

public class EmployeeHealthConditionVmHandler : VmQueryHandler, IRequestHandler<EmployeeHealthConditionVmQuery, EmployeeHealthConditionVm>
{
    public EmployeeHealthConditionVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeHealthConditionVm> Handle(EmployeeHealthConditionVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeeHealthConditions.AsQueryable().AsNoTracking();

        var entity = await queryable.SingleOrDefaultAsync(e => e.EmployeeHealthConditionId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<EmployeeHealthConditionVm>(entity);
    }
}