using Engage.Application.Services.Stores.Models;
using System.Globalization;

namespace Engage.Application.Services.Stores.Queries;

public class GetStoreBySearchAndEmployeeRegionQuery : IRequest<List<StoreListDto>>
{
    public string SearchTerm { get; set; }
    public int EmployeeId { get; set; }
}

public class GetStoreBySearchAndEmployeeRegionQueryHandler : BaseQueryHandler, IRequestHandler<GetStoreBySearchAndEmployeeRegionQuery, List<StoreListDto>>
{
    private IMediator _mediator;
    public GetStoreBySearchAndEmployeeRegionQueryHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper)
    {
        _mediator = mediator;
    }

    public async Task<List<StoreListDto>> Handle(GetStoreBySearchAndEmployeeRegionQuery request, CancellationToken cancellationToken)
    {
        if (request.EmployeeId == 0)
            return new List<StoreListDto>();

        var searchTerm = string.IsNullOrEmpty(request.SearchTerm) ? "" : request.SearchTerm.ToLower();

        var employeeRegions = _context.EmployeeRegions
                                        .Where(e => e.EmployeeId == request.EmployeeId)
                                        .Select(s => s.EngageRegionId)
                                        .ToList();

        // TODO: Move the All Regions to config file.
        if (employeeRegions.Contains(7)) // 7 is for all regions.
        {
            return await _mediator.Send(new GetStoreBySearchTermQuery() { SearchTerm = request.SearchTerm });
        }
        else
        {
            var stores = await _context.Stores.Join(_context.Locations,
                store => store.StakeholderId,
                location => location.StakeholderId,
                (store, location) => new { store, location })
                    .Where(x => x.store.Disabled == false && x.store.Deleted == false)
                    .Where(x => x.location.Disabled == false && x.location.Deleted == false)
                    .Where(x => employeeRegions.Contains(x.store.EngageRegionId))
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

    private bool IsValidLatitude(string lat)
    {
        string latitudeString = lat.Replace(",", ".").ToString();

        return decimal.TryParse(latitudeString, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out var latitude)
                      && Math.Abs(latitude) < 90;
    }

    private bool IsValidLongitude(string lon)
    {
        string longitudeString = lon.Replace(",", ".").ToString();

        return decimal.TryParse(longitudeString, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out var longitude)
                      && Math.Abs(longitude) < 90;
    }
}
