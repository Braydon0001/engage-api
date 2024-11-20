namespace Engage.Application.Services.EmployeeFileTypes.Queries;

public class EmployeeFileTypeListQuery : IRequest<List<EmployeeFileTypeDto>>
{

}

public class EmployeeFileTypeListHandler : ListQueryHandler, IRequestHandler<EmployeeFileTypeListQuery, List<EmployeeFileTypeDto>>
{
    public EmployeeFileTypeListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<EmployeeFileTypeDto>> Handle(EmployeeFileTypeListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeeFileTypes.AsQueryable().AsNoTracking();

        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<EmployeeFileTypeDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}