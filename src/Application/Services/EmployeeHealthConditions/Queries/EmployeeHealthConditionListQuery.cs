namespace Engage.Application.Services.EmployeeHealthConditions.Queries;

public class EmployeeHealthConditionListQuery : IRequest<List<EmployeeHealthConditionDto>>
{

}

public class EmployeeHealthConditionListHandler : ListQueryHandler, IRequestHandler<EmployeeHealthConditionListQuery, List<EmployeeHealthConditionDto>>
{
    public EmployeeHealthConditionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<EmployeeHealthConditionDto>> Handle(EmployeeHealthConditionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeeHealthConditions.AsQueryable().AsNoTracking();

        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<EmployeeHealthConditionDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}