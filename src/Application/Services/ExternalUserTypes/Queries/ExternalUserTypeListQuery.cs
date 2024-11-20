namespace Engage.Application.Services.ExternalUserTypes.Queries;

public class ExternalUserTypeListQuery : IRequest<List<ExternalUserTypeDto>>
{

}

public record ExternalUserTypeListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ExternalUserTypeListQuery, List<ExternalUserTypeDto>>
{
    public async Task<List<ExternalUserTypeDto>> Handle(ExternalUserTypeListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ExternalUserTypes.AsQueryable().AsNoTracking();

        return await queryable.OrderBy(e => e.ExternalUserTypeId)
                              .ProjectTo<ExternalUserTypeDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}