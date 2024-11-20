namespace Engage.Application.Services.CreditorFileTypes.Queries;

public class CreditorFileTypeListQuery : IRequest<List<CreditorFileTypeDto>>
{

}

public record CreditorFileTypeListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CreditorFileTypeListQuery, List<CreditorFileTypeDto>>
{
    public async Task<List<CreditorFileTypeDto>> Handle(CreditorFileTypeListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CreditorFileTypes.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<CreditorFileTypeDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}