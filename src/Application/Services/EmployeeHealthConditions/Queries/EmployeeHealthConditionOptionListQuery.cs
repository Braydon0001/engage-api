namespace Engage.Application.Services.EmployeeHealthConditions.Queries;

public class EmployeeHealthConditionOptionListQuery : IRequest<List<EmployeeHealthConditionOption>>
{

}

public class EmployeeHealthConditionOptionListHandler : ListQueryHandler, IRequestHandler<EmployeeHealthConditionOptionListQuery, List<EmployeeHealthConditionOption>>
{
    public EmployeeHealthConditionOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<EmployeeHealthConditionOption>> Handle(EmployeeHealthConditionOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeeHealthConditions.AsQueryable().AsNoTracking();

        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<EmployeeHealthConditionOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}