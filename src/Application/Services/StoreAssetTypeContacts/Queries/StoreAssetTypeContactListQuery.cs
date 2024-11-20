namespace Engage.Application.Services.StoreAssetTypeContacts.Queries;

public class StoreAssetTypeContactListQuery : IRequest<List<StoreAssetTypeContactDto>>
{

}

public record StoreAssetTypeContactListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<StoreAssetTypeContactListQuery, List<StoreAssetTypeContactDto>>
{
    public async Task<List<StoreAssetTypeContactDto>> Handle(StoreAssetTypeContactListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.StoreAssetTypeContacts.AsQueryable().AsNoTracking();

        return await queryable.OrderBy(e => e.StoreAssetTypeContactId)
                              .ProjectTo<StoreAssetTypeContactDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}