namespace Engage.Application.Services.AnalysisPillarSubGroups.Queries;

public class AnalysisPillarSubGroupListQuery : IRequest<List<AnalysisPillarSubGroupDto>>
{
}

public record AnalysisPillarSubGroupListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<AnalysisPillarSubGroupListQuery, List<AnalysisPillarSubGroupDto>>
{
    public async Task<List<AnalysisPillarSubGroupDto>> Handle(AnalysisPillarSubGroupListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.AnalysisPillarSubGroups.AsQueryable().AsNoTracking();

        return await queryable.OrderBy(e => e.AnalysisPillarSubGroupId)
                              .ProjectTo<AnalysisPillarSubGroupDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}