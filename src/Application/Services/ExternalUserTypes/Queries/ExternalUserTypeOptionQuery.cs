namespace Engage.Application.Services.ExternalUserTypes.Queries;

public class ExternalUserTypeOptionQuery : IRequest<List<ExternalUserTypeOption>>
{

}

public record ExternalUserTypeOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ExternalUserTypeOptionQuery, List<ExternalUserTypeOption>>
{
    public async Task<List<ExternalUserTypeOption>> Handle(ExternalUserTypeOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ExternalUserTypes.AsQueryable().AsNoTracking();

        return await queryable.OrderBy(e => e.ExternalUserTypeId)
                              .ProjectTo<ExternalUserTypeOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}