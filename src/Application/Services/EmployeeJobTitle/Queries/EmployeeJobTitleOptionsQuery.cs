namespace Engage.Application.Services.EmployeeJobTitles.Queries;

public class EmployeeJobTitleOptionsQuery : GetQuery, IRequest<List<OptionDto>>
{
    public int Level { get; set; }
}

public class EmployeeJobTitleOptionsHandler : IRequestHandler<EmployeeJobTitleOptionsQuery, List<OptionDto>>
{
    private readonly IAppDbContext _context;

    public EmployeeJobTitleOptionsHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<OptionDto>> Handle(EmployeeJobTitleOptionsQuery request, CancellationToken cancellationToken)
    {

        var queryable = _context.EmployeeJobTitles.Where(e => e.Disabled == false).AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            queryable = queryable.Where(e => EF.Functions.Like(e.Name, $"%{request.Search}%"));
        }

        return await queryable.Where(e => e.Level == request.Level && e.Disabled == false)
                              .OrderBy(e => e.Name)
                              .Select(e => new OptionDto(e.EmployeeJobTitleId, e.Name))
                              .ToListAsync(cancellationToken);

    }
}
