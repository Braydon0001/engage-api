namespace Engage.Application.Services.EmployeeFileTypes.Queries;

public class EmployeeFileTypeOptionListQuery : IRequest<List<EmployeeFileTypeOption>>
{

}

public class EmployeeFileTypeOptionListHandler : ListQueryHandler, IRequestHandler<EmployeeFileTypeOptionListQuery, List<EmployeeFileTypeOption>>
{
    public EmployeeFileTypeOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<EmployeeFileTypeOption>> Handle(EmployeeFileTypeOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeeFileTypes.AsQueryable().AsNoTracking();

        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<EmployeeFileTypeOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}