using Engage.Application.Services.DistributionCenters.Models;

namespace Engage.Application.Services.DistributionCenters.Queries
{
    public class DistributionCentersByStoreQuery : GetQuery, IRequest<List<DistibutionCenterAccountOptionDto>>
    {
        public int StoreId { get; set; }
    }

    public class DistributionCentersByStoreQueryHandler : BaseQueryHandler, IRequestHandler<DistributionCentersByStoreQuery, List<DistibutionCenterAccountOptionDto>>
    {
        public DistributionCentersByStoreQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<List<DistibutionCenterAccountOptionDto>> Handle(DistributionCentersByStoreQuery request, CancellationToken cancellationToken)
        {
            var dcs = await _context.DistributionCenters
                                 .Join(_context.DCAccounts,
                                       dc => dc.DistributionCenterId,
                                      dcAccount => dcAccount.DistributionCenterId,
                                      (dc, dcAccount) => new
                                      {
                                          Id = dc.DistributionCenterId,
                                          ParentId = dcAccount.StoreId,
                                          dcAccount.AccountNumber,
                                          dc.Name,
                                          dcAccount.Disabled,
                                          dcAccount.Deleted,
                                          dcAccountId = dcAccount.DCAccountId,
                                          isPrimary = dcAccount.IsPrimary
                                      })
                                 .Where(e => e.ParentId == request.StoreId
                                    && e.Disabled == false && e.Deleted == false)
                                 .OrderBy(e => e.AccountNumber)
                                 .ToListAsync(cancellationToken);


            var list = new List<DistibutionCenterAccountOptionDto>();

            foreach (var dc in dcs)
            {
                var option = new DistibutionCenterAccountOptionDto
                {
                    Id = dc.Id,
                    ParentId = dc.ParentId,
                    Name = $"{dc.Name} / {dc.AccountNumber}",
                    Disabled = dc.Disabled,
                    AccountId = dc.dcAccountId,
                    IsPrimary = dc.isPrimary
                };
                list.Add(option);
            }

            return list;
        }
    }
}
