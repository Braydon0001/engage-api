namespace Engage.Application.Services.Stores.Queries;

public class VoucherStoreOptionsQuery : GetQuery, IRequest<List<OptionDto>>
{
    public int VoucherId { get; set; }
}

public class VoucherStoreOptionsQueryHandler : IRequestHandler<VoucherStoreOptionsQuery, List<OptionDto>>
{
    private readonly IAppDbContext _context;

    public VoucherStoreOptionsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<OptionDto>> Handle(VoucherStoreOptionsQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.Stores.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            queryable = queryable.Where(e => (EF.Functions.Like(e.Name, $"%{request.Search}%"))
                                                || (EF.Functions.Like(e.Code, $"%{request.Search}%"))
                                                );
        }

        if (request.VoucherId != 0)
        {
            var voucher = await _context.Vouchers.SingleOrDefaultAsync(v => v.VoucherId == request.VoucherId);

            queryable = queryable.Where(e => e.EngageRegionId == voucher.EngageRegionId);
        }

        return await queryable.Where(e => e.Disabled == false)
                                  .Select(e => new OptionDto { Id = e.StoreId, Name = e.Name })
                                  .Take(100)
                                  .OrderBy(e => e.Name)
                                  .ToListAsync(cancellationToken);
    }
}