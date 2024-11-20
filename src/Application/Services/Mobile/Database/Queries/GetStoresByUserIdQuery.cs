using Engage.Application.Services.Mobile.Database.Models;

namespace Engage.Application.Services.Mobile.Database.Queries;

public class GetStoresByUserIdQuery : IRequest<List<StoreListDto>>
{
    public int EmployeeId { get; set; }
}

public class GetStoresByUserIdQueryHandler : BaseQueryHandler, IRequestHandler<GetStoresByUserIdQuery, List<StoreListDto>>
{
    public GetStoresByUserIdQueryHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper)
    {
    }

    public async Task<List<StoreListDto>> Handle(GetStoresByUserIdQuery request, CancellationToken cancellationToken)
    {
        if (request.EmployeeId == 0)
            return new List<StoreListDto>();

        var employeeRegions = _context.EmployeeRegions
                                        .Where(e => e.EmployeeId == request.EmployeeId)
                                        .Select(s => s.EngageRegionId)
                                        .ToList();

        if (employeeRegions.Contains(7)) // 7 is for all regions.
        {
            var stores = await _context.Stores.Join(_context.Locations,
            store => store.StakeholderId,
            location => location.StakeholderId,
            (store, location) => new { store, location })
                .Where(x => x.store.Disabled == false && x.store.Deleted == false)
                .Where(x => x.location.Disabled == false && x.location.Deleted == false)
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
                    //Email = x.store.PrimaryContact.PrimaryEmailContactItem.Value,
                    //Mobile = x.store.PrimaryContact.PrimaryMobileContactItem.Value,
                    Lat = x.location.Lat,
                    Long = x.location.Long,
                    Distance = -1,
                    Contacts = x.store.StoreContacts.Where(e => e.Store.StoreId == x.store.StoreId).Select(o => new MobileStoreContactDto() { Name = o.FullName, Title = o.EntityContactType.Name, Email = o.EmailAddress1, PhoneNumber = o.MobilePhone }).ToList(),
                })
            .ToListAsync();

            return stores;
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
                    Lat = x.location.Lat,
                    Long = x.location.Long,
                    Distance = -1,
                    Contacts = x.store.StoreContacts.Where(e => e.Store.StoreId == x.store.StoreId).Select(o => new MobileStoreContactDto() { Name = o.FullName, Title = o.EntityContactType.Name, Email = o.EmailAddress1, PhoneNumber = o.MobilePhone }).ToList(),
                })
            .ToListAsync();

            return stores;

        }
    }
}
