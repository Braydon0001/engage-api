using Engage.Application.Services.Stores.Models;

namespace Engage.Application.Services.Stores.Queries;

public class GetStoreBySearchTermQuery : IRequest<List<StoreListDto>>
{
    public string SearchTerm { get; set; }
}

public class GetStoreBySearchTermQueryHandler : BaseQueryHandler, IRequestHandler<GetStoreBySearchTermQuery, List<StoreListDto>>
{
    public GetStoreBySearchTermQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<StoreListDto>> Handle(GetStoreBySearchTermQuery request, CancellationToken cancellationToken)
    {
        var searchTerm = string.IsNullOrEmpty(request.SearchTerm) ? "" : request.SearchTerm.ToLower();

        var stores = await _context.Stores.Join(_context.Locations,
        store => store.StakeholderId,
        location => location.StakeholderId,
        (store, location) => new { store, location })
            .Where(x => x.store.Disabled == false)
            .Where(x => x.location.Disabled == false)
            .Where(x => x.store.Name.ToLower().Contains(searchTerm))
            .Select(x => new StoreListDto
            {
                Id = x.store.StoreId,
                Code = x.store.Code,
                Name = x.store.Name,
                EngageRegionName = x.store.EngageRegion.Name,
                StoreImageUrl = x.store.StoreImageUrl,
                StoreTypeImageUrl = x.store.StoreImageUrl,
                AddressLineOne = x.location.AddressLineOne,
                AddressLineTwo = x.location.AddressLineTwo,
                Suburb = x.location.Suburb,
                City = x.location.City,
                Province = x.location.Province,
                PostCode = x.location.PostCode,
                Email = x.store.PrimaryContact.PrimaryEmailContactItem.Value,
                Mobile = x.store.PrimaryContact.PrimaryMobileContactItem.Value,
                Lat = x.location.Lat,
                Long = x.location.Long,
                Distance = -1
            })
            .ToListAsync();

        return stores;
    }
}
