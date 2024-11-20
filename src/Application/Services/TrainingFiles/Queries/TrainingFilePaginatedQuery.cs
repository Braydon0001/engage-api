namespace Engage.Application.Services.TrainingFiles.Queries;

public class TrainingFilePaginatedQuery : PaginatedQuery, IRequest<ListResult<TrainingFileDto>>
{
}

public record TrainingFilePaginatedHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<TrainingFilePaginatedQuery, ListResult<TrainingFileDto>>
{
    public async Task<ListResult<TrainingFileDto>> Handle(TrainingFilePaginatedQuery query, CancellationToken cancellationToken)
    {
        var props = TrainingFilePaginationProps.Create();

        var queryable = Context.TrainingFiles.AsQueryable().AsNoTracking();

        if (query.SortModel.IsNullOrEmpty())
        {
            queryable = queryable.OrderByDescending(e => e.TrainingFileId);
        }

        var entities = await queryable.Filter(query, props)
                                      .Sort(query, props)
                                      .SkipQuery(query)
                                      .TakeQuery(query)
                                      .ProjectTo<TrainingFileDto>(Mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return new(entities);
    }
}


