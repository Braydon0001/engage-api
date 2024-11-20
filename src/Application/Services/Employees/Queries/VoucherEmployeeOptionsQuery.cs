namespace Engage.Application.Services.Employees.Queries;

public class VoucherEmployeeOptionsQuery : GetQuery, IRequest<List<OptionDto>>
{
    public int VoucherId { get; set; }
}

public class VoucherEmployeeOptionsQueryHandler : IRequestHandler<VoucherEmployeeOptionsQuery, List<OptionDto>>
{
    private readonly IAppDbContext _context;

    public VoucherEmployeeOptionsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<OptionDto>> Handle(VoucherEmployeeOptionsQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.Employees.Where(e => e.EmployeeTypeId == (int)EmployeeTypeId.Employee)
                                          .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            queryable = queryable.Where(e => (EF.Functions.Like(e.FirstName, $"%{request.Search}%"))
                                                || (EF.Functions.Like(e.LastName, $"%{request.Search}%"))
                                                || (EF.Functions.Like(e.Code, $"%{request.Search}%"))
                                                );
        }

        if (request.VoucherId != 0)
        {
            var voucher = await _context.Vouchers.SingleOrDefaultAsync(v => v.VoucherId == request.VoucherId);

            queryable = queryable.Where(e => e.EmployeeRegions.Select(e => e.EngageRegionId).Contains(voucher.EngageRegionId));
        }

        return await queryable.Where(e => e.Disabled == false)
                                  .Select(e => new OptionDto { Id = e.EmployeeId, Name = e.FirstName + " " + e.LastName + " - " + e.Code })
                                  .Take(100)
                                  .OrderBy(e => e.Name)
                                  .ToListAsync(cancellationToken);
    }
}