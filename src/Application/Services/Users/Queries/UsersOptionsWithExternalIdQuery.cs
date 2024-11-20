namespace Engage.Application.Services.Users.Queries;

public class UsersOptionsWithExternalIdQuery : IRequest<List<OptionDto>>
{
    public string Search { get; set; }
}
public record UsersOptionsWithExternalIdHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<UsersOptionsWithExternalIdQuery, List<OptionDto>>
{
    public async Task<List<OptionDto>> Handle(UsersOptionsWithExternalIdQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.Users.AsNoTracking()
                                     .AsQueryable();

        queryable = queryable.Where(e => e.ExternalId != null);

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            queryable = queryable.Where(e => EF.Functions.Like(e.FirstName, $"%{query.Search}%")
                                          || EF.Functions.Like(e.LastName, $"%{query.Search}%"));
        }
        else
        {
            queryable = queryable.Take(50);
        }

        return await queryable.Where(e => e.Disabled == false)
                              .OrderBy(e => e.FullName)
                              .Select(e => new OptionDto(e.UserId, e.FullName))
                              .ToListAsync(cancellationToken);
    }
}
