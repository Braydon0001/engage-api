namespace Engage.Application.Services.EmployeeJobTitleTimes.Queries;

public class EmployeeJobTitleTimeOptionsQuery : GetQuery, IRequest<List<EmployeeJobTitleTimeOption>>
{
    public int? EmployeeJobTitleId { get; set; }
}

public record EmployeeJobTitleTimeOptionsHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<EmployeeJobTitleTimeOptionsQuery, List<EmployeeJobTitleTimeOption>>
{
    public async Task<List<EmployeeJobTitleTimeOption>> Handle(EmployeeJobTitleTimeOptionsQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.EmployeeJobTitleTimes.AsQueryable()
                                                     .AsNoTracking();

        if (query.EmployeeJobTitleId.HasValue)
        {
            queryable = queryable.Where(e => e.EmployeeJobTitleId == query.EmployeeJobTitleId.Value);
        }

        return await queryable.ProjectTo<EmployeeJobTitleTimeOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}