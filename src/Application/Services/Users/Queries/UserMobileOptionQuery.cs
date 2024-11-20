namespace Engage.Application.Services.Users.Queries;

public class UserMobileOptionQuery : PaginatedQuery, IRequest<List<OptionDto>>
{
    public string Search { get; set; }
}

public class UserMobileOptionQueryHandler : ListQueryHandler, IRequestHandler<UserMobileOptionQuery, List<OptionDto>>
{
    public UserMobileOptionQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<OptionDto>> Handle(UserMobileOptionQuery query, CancellationToken cancellationToken)
    {
        var props = UserPaginationProps.Create();

        var queryable = _context.Users.AsQueryable().AsNoTracking();

        if (query.Search.IsNotNullOrWhiteSpace())
        {
            queryable = queryable.Where(e => EF.Functions.Like(e.FullName, $"%{query.Search}%")
                                          || EF.Functions.Like(e.Email, $"%{query.Search}%"));
        }

        if (query.SortModel.IsNullOrEmpty())
        {
            queryable = queryable.OrderBy(e => e.FullName);
        }

        var entities = await queryable.Filter(query, props)
                                      .Sort(query, props)
                                      .SkipQuery(query)
                                      .TakeQuery(query)
                                      .Select(e => new OptionDto(e.UserId, e.FullName + " - " + e.Email))
                                      .ToListAsync(cancellationToken);

        return entities;
    }
}
