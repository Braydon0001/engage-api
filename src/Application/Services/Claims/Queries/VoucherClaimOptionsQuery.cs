namespace Engage.Application.Services.Claims.Queries;

public class VoucherClaimOptionsQuery : GetQuery, IRequest<List<OptionDto>>
{
    public int VoucherId { get; set; }
}

public class VoucherClaimOptionsQueryHandler : IRequestHandler<VoucherClaimOptionsQuery, List<OptionDto>>
{
    private readonly IAppDbContext _context;
    private readonly ClaimSettings _claimSettings;

    public VoucherClaimOptionsQueryHandler(IAppDbContext context, IOptions<ClaimSettings> claimSettings)
    {
        _context = context;
        _claimSettings = claimSettings.Value;
    }

    public async Task<List<OptionDto>> Handle(VoucherClaimOptionsQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.Claims.Where(c => c.ClaimTypeId == _claimSettings.ClaimVoucherTypeId &&
                                                  (c.ClaimStatusId == (int)ClaimStatusId.Approved ||
                                                   c.ClaimStatusId == (int)ClaimStatusId.Paid ||
                                                   c.ClaimStatusId == (int)ClaimStatusId.Unapproved)).AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            queryable = queryable.Where(e => (EF.Functions.Like(e.ClaimNumber, $"%{request.Search}%"))
                                            || (EF.Functions.Like(e.ClaimReference, $"%{request.Search}%"))
                                            );
        }

        if (request.VoucherId != 0)
        {
            var voucher = await _context.Vouchers.SingleOrDefaultAsync(v => v.VoucherId == request.VoucherId);

            queryable = queryable.Where(e => e.Store.EngageRegionId == voucher.EngageRegionId);
        }

        return await queryable.Where(e => e.Disabled == false)
                              .Select(e => new OptionDto { Id = e.ClaimId, Name = e.ClaimNumber + " - " + e.Store.Name })
                              .Take(100)
                              .OrderBy(e => e.Name)
                              .ToListAsync(cancellationToken);
    }
}