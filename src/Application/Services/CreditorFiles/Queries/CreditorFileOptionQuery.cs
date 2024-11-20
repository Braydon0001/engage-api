namespace Engage.Application.Services.CreditorFiles.Queries;

public class CreditorFileOptionQuery : IRequest<List<CreditorFileOption>>
{ 

}

public record CreditorFileOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CreditorFileOptionQuery, List<CreditorFileOption>>
{
    public async Task<List<CreditorFileOption>> Handle(CreditorFileOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CreditorFiles.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.CreditorFileId)
                              .ProjectTo<CreditorFileOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}