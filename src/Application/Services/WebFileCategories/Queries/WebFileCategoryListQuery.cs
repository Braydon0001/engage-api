namespace Engage.Application.Services.WebFileCategories.Queries;

public class WebFileCategoryListQuery : IRequest<List<WebFileCategoryDto>>
{
    public int? WebFileGroupId { get; set; }
}

public class WebFileCategoryListHandler : ListQueryHandler, IRequestHandler<WebFileCategoryListQuery, List<WebFileCategoryDto>>
{
    public WebFileCategoryListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<WebFileCategoryDto>> Handle(WebFileCategoryListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.WebFileCategories.AsQueryable().AsNoTracking();

        if (query.WebFileGroupId.HasValue)
        {
            queryable = queryable.Where(e => e.WebFileGroupId == query.WebFileGroupId);
        }

        return await queryable.OrderBy(e => e.DisplayName)
                              .ProjectTo<WebFileCategoryDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}
