namespace Engage.Application.Services.CreditorStatuses.Queries;

public class CreditorStatusListQuery : IRequest<List<CreditorStatusDto>>
{

}

public record CreditorStatusListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CreditorStatusListQuery, List<CreditorStatusDto>>
{
    public async Task<List<CreditorStatusDto>> Handle(CreditorStatusListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CreditorStatuses.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.CreditorStatusId)
                              .ProjectTo<CreditorStatusDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}