namespace Engage.Application.Services.EmployeeJobTitles.Queries;

public class EmployeeJobTitlesQuery : GetQuery, IRequest<ListResult<EmployeeJobTitleDto>>
{
    public int? Level { get; set; }
}

public class EmployeeJobTitlesHandler : BaseQueryHandler, IRequestHandler<EmployeeJobTitlesQuery, ListResult<EmployeeJobTitleDto>>
{
    public EmployeeJobTitlesHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<EmployeeJobTitleDto>> Handle(EmployeeJobTitlesQuery request, CancellationToken cancellationToken)
    {

        var queryable = _context.EmployeeJobTitles.AsQueryable();

        if (request.Level.HasValue)
        {
            queryable = queryable.Where(e => e.Level == request.Level);
        }

        var jobTitles = await queryable.OrderBy(e => e.Name)
                                      .ProjectTo<EmployeeJobTitleDto>(_mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return new ListResult<EmployeeJobTitleDto>(jobTitles);
    }
}
