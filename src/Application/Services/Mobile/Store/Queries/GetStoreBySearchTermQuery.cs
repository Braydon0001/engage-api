using Engage.Application.Services.Mobile.Database.Models;
using Geolocation;

namespace Engage.Application.Services.Mobile.Stores.Queries;

public class GetStoreBySearchTermQuery : IRequest<List<StoreListDto>>
{
    public string SearchTerm { get; set; }
    public double? Lat { get; set; }
    public double? Lon { get; set; }
}

public class GetStoreBySearchTermQueryHandler : BaseQueryHandler, IRequestHandler<GetStoreBySearchTermQuery, List<StoreListDto>>
{
    public GetStoreBySearchTermQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<StoreListDto>> Handle(GetStoreBySearchTermQuery request, CancellationToken cancellationToken)
    {
        var searchTerm = string.IsNullOrEmpty(request.SearchTerm) ? "" : request.SearchTerm.ToLower();

        var gps = new Coordinate();
        var hasGps = false;

        if (request.Lat.HasValue && request.Lat.HasValue)
        {
            Coordinate origin = new Coordinate() { Latitude = request.Lat.Value, Longitude = request.Lon.Value };
            gps = origin;
            hasGps = true;
        }

        var stores = await _context.Stores.Join(_context.Locations,
        store => store.StakeholderId,
        location => location.StakeholderId,
        (store, location) => new { store, location })
            .Where(x => x.store.Disabled == false)
            .Where(x => x.location.Disabled == false)
            .Where(x => x.location.IsPrimaryAddress == true)
            .Where(x => x.store.Name.ToLower().Contains(searchTerm))
            .Select(x => new StoreListDto
            {
                Id = x.store.StoreId,
                Code = x.store.Code,
                Name = x.store.Name,
                EngageRegionName = x.store.EngageRegion.Name,
                StoreImageUrl = x.store.StoreImageUrl,
                AddressLineOne = x.location.AddressLineOne,
                AddressLineTwo = x.location.AddressLineTwo,
                Suburb = x.location.Suburb,
                City = x.location.City,
                Province = x.location.Province,
                PostCode = x.location.PostCode,
                Lat = x.location.Lat,
                Long = x.location.Long,
                Distance = hasGps ? (
                                    GeoCalculator.GetDistance(
                                    gps.Latitude,
                                    gps.Longitude,
                                    Convert.ToDouble(x.location.Lat),
                                    Convert.ToDouble(x.location.Long), 2,
                                    DistanceUnit.Kilometers)
                                    ) : (-1),
                Contacts = x.store.StoreContacts.Where(e => e.Store.StoreId == x.store.StoreId).Select(o => new MobileStoreContactDto() { Name = o.FullName, Title = o.EntityContactType.Name, Email = o.EmailAddress1, PhoneNumber = o.MobilePhone }).ToList(),
                DistributionCentres = x.store.DCAccounts.OrderBy(e => e.AccountNumber).Select(e => new OptionDto() { Name = $"{e.DistributionCenter.Name} - {e.AccountNumber}", Id = e.DistributionCenterId }).ToList()
            })
            .ToListAsync();

        return stores;
    }
}
