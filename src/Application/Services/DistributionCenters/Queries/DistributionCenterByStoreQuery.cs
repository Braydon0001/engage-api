namespace Engage.Application.Services.DistributionCenters.Queries;

public class DistributionCenterByStoreQuery : GetQuery, IRequest<OptionDto>
{
    public int DistributionCenterId { get; set; }
    public int StoreId { get; set; }
}

public class DistributionCenterByStoreQueryHandler : BaseQueryHandler, IRequestHandler<DistributionCenterByStoreQuery, OptionDto>
{
    public DistributionCenterByStoreQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OptionDto> Handle(DistributionCenterByStoreQuery request, CancellationToken cancellationToken)
    {
        var dc = await _context.DistributionCenters
                             .Join(_context.DCAccounts,
                                   dc => dc.DistributionCenterId,
                                  dcAccount => dcAccount.DistributionCenterId,
                                  (dc, dcAccount) => new
                                  {
                                      dc.DistributionCenterId,
                                      dc.Name,
                                      dcAccount.StoreId,
                                      dcAccount.AccountNumber,
                                  })
                             .FirstOrDefaultAsync(e =>
                                         e.DistributionCenterId == request.DistributionCenterId &&
                                         e.StoreId == request.StoreId,
                                         cancellationToken);

        return new OptionDto
        {
            Id = dc.DistributionCenterId,
            Name = $"{dc.Name} / {dc.AccountNumber}"
        };
    }
}
