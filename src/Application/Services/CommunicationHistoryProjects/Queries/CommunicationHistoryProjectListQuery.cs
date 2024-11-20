namespace Engage.Application.Services.CommunicationHistoryProjects.Queries;

public class CommunicationHistoryProjectListQuery : IRequest<List<CommunicationHistoryProjectDto>>
{
    public int? ProjectId { get; set; }
}

public record CommunicationHistoryProjectListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CommunicationHistoryProjectListQuery, List<CommunicationHistoryProjectDto>>
{
    public async Task<List<CommunicationHistoryProjectDto>> Handle(CommunicationHistoryProjectListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CommunicationHistoryProjects.AsQueryable().AsNoTracking();

        if (query.ProjectId.HasValue)
        {
            queryable = queryable.Where(e => e.ProjectId == query.ProjectId.Value);
        }

        return await queryable.OrderBy(e => e.CommunicationHistoryId)
                              .ProjectTo<CommunicationHistoryProjectDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}