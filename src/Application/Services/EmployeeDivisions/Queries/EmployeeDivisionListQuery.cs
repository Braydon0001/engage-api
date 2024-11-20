namespace Engage.Application.Services.EmployeeDivisions.Queries;

public class EmployeeDivisionListQuery : IRequest<List<EmployeeDivisionDto>>
{

}

public class EmployeeDivisionListHandler : ListQueryHandler, IRequestHandler<EmployeeDivisionListQuery, List<EmployeeDivisionDto>>
{
    public EmployeeDivisionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<EmployeeDivisionDto>> Handle(EmployeeDivisionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeeDivisions.AsQueryable().AsNoTracking();

        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<EmployeeDivisionDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}