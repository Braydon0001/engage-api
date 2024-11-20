namespace Engage.Application.Services.EmployeeJobTitleTypes.Queries;

public class EmployeeJobTitleTypeOptionsQuery : GetQuery, IRequest<List<EmployeeJobTitleTypeOption>>
{
    public int? EmployeeJobTitleId { get; set; }
}

public record EmployeeJobTitleTypeOptionsHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<EmployeeJobTitleTypeOptionsQuery, List<EmployeeJobTitleTypeOption>>
{
    public async Task<List<EmployeeJobTitleTypeOption>> Handle(EmployeeJobTitleTypeOptionsQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.EmployeeJobTitleTypes.AsQueryable()
                                                     .AsNoTracking();

        if (query.EmployeeJobTitleId.HasValue)
        {
            queryable = queryable.Where(e => e.EmployeeJobTitleId == query.EmployeeJobTitleId.Value);
        }

        return await queryable.ProjectTo<EmployeeJobTitleTypeOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}