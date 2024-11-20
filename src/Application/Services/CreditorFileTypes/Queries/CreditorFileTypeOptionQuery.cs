namespace Engage.Application.Services.CreditorFileTypes.Queries;

public class CreditorFileTypeOptionQuery : IRequest<List<CreditorFileTypeOption>>
{ 

}

public record CreditorFileTypeOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CreditorFileTypeOptionQuery, List<CreditorFileTypeOption>>
{
    public async Task<List<CreditorFileTypeOption>> Handle(CreditorFileTypeOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CreditorFileTypes.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<CreditorFileTypeOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}