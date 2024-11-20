using Engage.Application.Services.Mobile.Database.Models;

namespace Engage.Application.Services.Mobile.Stores.Queries;

public class GetStoreByEmployeeRegionQuery : IRequest<List<StoreListDto>>
{
    public int EmployeeId { get; set; }
}

public class GetStoreByEmployeeRegionQueryHandler : BaseQueryHandler, IRequestHandler<GetStoreByEmployeeRegionQuery, List<StoreListDto>>
{
    private IMediator _mediator;
    public GetStoreByEmployeeRegionQueryHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper)
    {
        _mediator = mediator;
    }

    public async Task<List<StoreListDto>> Handle(GetStoreByEmployeeRegionQuery request, CancellationToken cancellationToken)
    {
        if (request.EmployeeId == 0)
            return new List<StoreListDto>();

        var employeeRegions = _context.EmployeeRegions
                                        .Where(e => e.EmployeeId == request.EmployeeId)
                                        .Select(s => s.EngageRegionId)
                                        .ToList();

        var stores = await _context.Stores.Join(_context.Locations,
            store => store.StakeholderId,
            location => location.StakeholderId,
            (store, location) => new { store, location })
                .Where(x => x.store.Disabled == false && x.store.Deleted == false)
                .Where(x => x.location.Disabled == false && x.location.Deleted == false)
                .Where(x => x.location.IsPrimaryAddress == true)
                .Select(x => new StoreListDto
                {
                    Id = x.store.StoreId,
                    StoreFormatId = x.store.StoreFormatId,
                    Code = x.store.Code,
                    Name = x.store.Name,
                    EngageRegionId = x.store.EngageRegionId,
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
                    Distance = -1,
                    Contacts = x.store.StoreContacts.Where(e => e.Store.StoreId == x.store.StoreId).Select(o => new MobileStoreContactDto() { Name = o.FullName, Title = o.EntityContactType.Name, Email = o.EmailAddress1, PhoneNumber = o.MobilePhone }).ToList(),
                    DistributionCentres = x.store.DCAccounts.OrderBy(e => e.AccountNumber).Select(e => new OptionDto() { Name = $"{e.DistributionCenter.Name} - {e.AccountNumber}", Id = e.DistributionCenterId }).ToList()
                })
                .ToListAsync();

        var uniqueStores = stores.DistinctBy(s => s.Id).OrderBy(s => s.Id).ToList();

        if (!employeeRegions.Contains(7)) // 7 is for all regions.
        {
            uniqueStores = uniqueStores.Where(x => employeeRegions.Contains(x.EngageRegionId)).ToList();
        }

        return uniqueStores;
    }
}
