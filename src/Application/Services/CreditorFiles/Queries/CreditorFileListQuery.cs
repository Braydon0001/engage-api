namespace Engage.Application.Services.CreditorFiles.Queries;

public class CreditorFileListQuery : IRequest<List<CreditorFileDto>>
{

}

public record CreditorFileListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CreditorFileListQuery, List<CreditorFileDto>>
{
    public async Task<List<CreditorFileDto>> Handle(CreditorFileListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CreditorFiles.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.CreditorFileId)
                              .ProjectTo<CreditorFileDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}