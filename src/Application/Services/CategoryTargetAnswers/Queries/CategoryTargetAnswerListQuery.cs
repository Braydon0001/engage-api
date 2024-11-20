namespace Engage.Application.Services.CategoryTargetAnswers.Queries;

public class CategoryTargetAnswerListQuery : IRequest<List<CategoryTargetAnswerDto>>
{
    public int StoreId { get; set; }
}

public record CategoryTargetAnswerHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CategoryTargetAnswerListQuery, List<CategoryTargetAnswerDto>>
{
    public async Task<List<CategoryTargetAnswerDto>> Handle(CategoryTargetAnswerListQuery query, CancellationToken cancellationToken)
    {

        var CategoryTargetIds = await Context.CategoryTargetStores.Where(e => e.StoreId == query.StoreId
                                                                                && e.CategoryTarget.Disabled == false)
                                                                  .ToListAsync(cancellationToken);


        var entities = Context.CategoryTargetAnswers.AsQueryable().AsNoTracking()
        .Where(e => CategoryTargetIds
        .Select(s => s.CategoryTargetStoreId)
        .Contains(e.CategoryTargetStoreId));




        return await entities
            .OrderBy(e => e.CategoryTargetId)
            .ProjectTo<CategoryTargetAnswerDto>(Mapper.ConfigurationProvider)
            .ToListAsync();

    }
}
