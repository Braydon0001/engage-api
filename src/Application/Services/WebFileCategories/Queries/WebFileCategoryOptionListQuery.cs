namespace Engage.Application.Services.WebFileCategories.Queries;

public class WebFileCategoryOptionListQuery : IRequest<List<WebFileCategoryOption>>
{

}

public class WebFileCategoryOptionListHandler : ListQueryHandler, IRequestHandler<WebFileCategoryOptionListQuery, List<WebFileCategoryOption>>
{
    public WebFileCategoryOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<WebFileCategoryOption>> Handle(WebFileCategoryOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.WebFileCategories.AsQueryable().AsNoTracking();

        return await queryable.OrderBy(e => e.WebFileCategoryId)
                              .ProjectTo<WebFileCategoryOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}