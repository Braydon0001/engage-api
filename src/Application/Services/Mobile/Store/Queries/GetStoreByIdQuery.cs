using Engage.Application.Services.Mobile.Database.Models;

namespace Engage.Application.Services.Mobile.Stores.Queries;

public class GetStoreByIdQuery : IRequest<StoreListDto>
{
    public int StoreId { get; set; }
}

public class GetStoreByIdQueryHandler : BaseQueryHandler, IRequestHandler<GetStoreByIdQuery, StoreListDto>
{
    private IMediator _mediator;
    public GetStoreByIdQueryHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper)
    {
        _mediator = mediator;
    }

    public async Task<StoreListDto> Handle(GetStoreByIdQuery request, CancellationToken cancellationToken)
    {
        if (request.StoreId == 0)
            return new StoreListDto();

        var stores = await _context.Stores.Join(_context.Locations,
            store => store.StakeholderId,
            location => location.StakeholderId,
            (store, location) => new { store, location })
                .Where(x => x.store.Disabled == false && x.store.Deleted == false)
                .Where(x => x.location.Disabled == false && x.location.Deleted == false)
                .Where(x => x.location.IsPrimaryAddress == true)
                .Where(x => x.store.StoreId == request.StoreId)
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
                    //Email = x.store.PrimaryContact.PrimaryEmailContactItem.Value,
                    //Mobile = x.store.PrimaryContact.PrimaryMobileContactItem.Value,
                    Lat = x.location.Lat,
                    Long = x.location.Long,
                    Distance = -1,
                    Contacts = x.store.StoreContacts.Where(e => e.Store.StoreId == x.store.StoreId).Select(o => new MobileStoreContactDto() { Name = o.FullName, Title = o.EntityContactType.Name, Email = o.EmailAddress1, PhoneNumber = o.MobilePhone }).ToList(),
                    DistributionCentres = x.store.DCAccounts.OrderBy(e => e.AccountNumber).Select(e => new OptionDto() { Name = $"{e.DistributionCenter.Name} - {e.AccountNumber}", Id = e.DistributionCenterId }).ToList()
                })
            .FirstOrDefaultAsync();

        return stores;
    }
}
