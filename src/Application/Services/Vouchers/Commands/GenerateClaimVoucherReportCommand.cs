using Engage.Application.Services.ClaimReports.Models;
using Engage.Application.Services.Vouchers.Models;

namespace Engage.Application.Services.Vouchers.Commands;

public class GenerateClaimVoucherReportCommand : GetQuery, IRequest<ReportListVM<ClaimVoucherReportDto>>
{
    public List<int> EngageRegionIds { get; set; }
    public int ClaimPeriodId { get; set; }
}

public class GenerateClaimVoucherReportCommandHandler : BaseQueryHandler, IRequestHandler<GenerateClaimVoucherReportCommand, ReportListVM<ClaimVoucherReportDto>>
{
    private readonly ClaimSettings _claimSettings;
    public GenerateClaimVoucherReportCommandHandler(IAppDbContext context, IMapper mapper, IOptions<ClaimSettings> claimSettings) : base(context, mapper)
    {
        _claimSettings = claimSettings.Value;
    }

    public async Task<ReportListVM<ClaimVoucherReportDto>> Handle(GenerateClaimVoucherReportCommand command, CancellationToken cancellationToken)
    {
        var claimPeriod = await _context.ClaimPeriods.Include(c => c.ClaimYear)
                                                     .SingleOrDefaultAsync(c => c.ClaimPeriodId == command.ClaimPeriodId, cancellationToken);

        var query = _context.ClaimSkus
                                      .Where(e => e.Claim.ClaimTypeId == _claimSettings.ClaimVoucherTypeId &&
                                                 (e.Claim.ClaimStatusId == (int)ClaimStatusId.Approved ||
                                                  e.Claim.ClaimStatusId == (int)ClaimStatusId.Paid));

        var vouchersQuery = _context.VoucherDetails.AsQueryable();
        vouchersQuery = vouchersQuery.Where(e => e.VoucherDetailStatusId == (int)VoucherDetailStatusId.Issued
                                              && e.Claim.ApprovedDate.Value.Date >= claimPeriod.StartDate.Date
                                              && e.Claim.ApprovedDate.Value.Date <= claimPeriod.EndDate.Date);

        if (command.EngageRegionIds != null && command.EngageRegionIds.Any())
        {
            query = query.Where(e => command.EngageRegionIds.Contains(e.Claim.Store.EngageRegionId)).AsQueryable();
            vouchersQuery = vouchersQuery.Where(e => command.EngageRegionIds.Contains(e.Voucher.EngageRegionId)).AsQueryable();
        }
        query = query.Where(e => (e.Claim.ApprovedDate.Value.Date >= claimPeriod.StartDate.Date && e.Claim.ApprovedDate.Value.Date <= claimPeriod.EndDate.Date));

        var vouchers = await vouchersQuery
            .OrderBy(c => c.VoucherDetailId)
            .Include(e => e.Voucher)
            .ProjectTo<VoucherReportDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);


        var entities = await query
                                  .ProjectTo<ClaimVoucherReportDto>(_mapper.ConfigurationProvider)
                                  .ToListAsync(cancellationToken);



        var data = entities.Select(c => new ClaimVoucherReportDto
        {
            StoreId = c.StoreId,
            StoreName = c.StoreName,
            ClaimNumber = c.ClaimNumber,
            IssuedDate = GetVoucherIssuedDate(vouchers, c.ClaimNumber),
            IssuedBy = GetVoucherIssuedBy(vouchers, c.ClaimNumber),
            ClaimReference = c.ClaimReference,
            Quantity = c.Quantity,
            CreatedDate = c.CreatedDate,
            ClaimDate = c.ClaimDate,
            ProductName = c.ProductName,
            ClaimSupplierName = c.ClaimSupplierName,
            ProductSupplierName = c.ProductSupplierName,
            ClaimTypeName = c.ClaimTypeName,
            ClaimAccountManagerName = c.ClaimAccountManagerName,
            ClaimManagerName = c.ClaimManagerName,
            IsPayStore = c.IsPayStore,
            IsClaimFromSupplier = c.IsClaimFromSupplier,
            Amount = c.Amount,
            TotalAmount = c.TotalAmount,
            StoreClaimNumberTotal = GetClaimStoreTotal(entities, c.StoreId, c.ClaimId, c.ProductCreatedDate),
            StoreTotal = GetStoreTotal(entities, c.StoreId, c.ClaimId, c.ProductCreatedDate),
            Note = c.Note,
            CreatedBy = c.CreatedBy,
            VatAmount = c.VatAmount,
            ClaimId = c.ClaimId,
        })
                            .OrderBy(c => c.StoreName)
                            .ThenBy(c => c.ClaimNumber)
                            .ThenBy(c => c.ProductCreatedDate)
                            .ToList();

        var claimTypeTotals = data
                              .GroupBy(c => c.ClaimTypeName)
                              .Select(c => new { Amount = c.Sum(b => b.TotalAmount), ClaimTypeName = c.Key })
                              .OrderByDescending(c => c.Amount)
                              .ToList();

        var vatTotal = data.Sum(c => c.VatAmount);
        var reportTotal = data.Sum(c => c.TotalAmount);
        var totalExcVat = reportTotal - vatTotal;

        var Totals = new List<ReportSubTotal>();
        Totals.Add(new ReportSubTotal { Name = "TOTAL EXCL. VAT", Value = totalExcVat });
        Totals.Add(new ReportSubTotal { Name = "TOTAL VAT", Value = vatTotal });

        if (claimTypeTotals.Count > 0)
        {
            foreach (var typeTotal in claimTypeTotals)
            {
                Totals.Add(new ReportSubTotal { Name = typeTotal.ClaimTypeName.ToUpper(), Value = typeTotal.Amount });
            }
        }

        Totals.Add(new ReportSubTotal { Name = "REPORT TOTAL", Value = Math.Round(reportTotal, 2) });

        string reportName = "Finance Report" + " - " + DateTime.Now.ToString();

        return new ReportListVM<ClaimVoucherReportDto>
        {
            Count = data.Count,
            ReportName = reportName,
            Data = data,
            ReportSubTotals = Totals,
        };
    }

    private decimal GetStoreTotal(List<ClaimVoucherReportDto> list, int StoreId, int ClaimId, DateTime CreatedDate)
    {
        var line = list.Where(i => i.StoreId == StoreId)
                       .OrderByDescending(s => s.ClaimId)
                       .ThenByDescending(s => s.ProductCreatedDate)
                       .First();

        if (ClaimId == line.ClaimId && CreatedDate == line.ProductCreatedDate)
        {
            return list.Where(i => i.StoreId == StoreId)
                       .Sum(s => (s.TotalAmount));
        }

        return 0;
    }

    private decimal GetClaimStoreTotal(List<ClaimVoucherReportDto> list, int StoreId, int ClaimId, DateTime CreatedDate)
    {
        var date = list.Where(i => i.StoreId == StoreId && i.ClaimId == ClaimId)
                       .OrderByDescending(s => s.ClaimId)
                       .ThenByDescending(s => s.ProductCreatedDate)
                       .Select(s => s.ProductCreatedDate)
                       .First();

        if (CreatedDate == date)
        {
            return list.Where(i => i.StoreId == StoreId && i.ClaimId == ClaimId)
                       .Sum(s => (s.TotalAmount));
        }

        return 0;
    }

    private string GetVoucherIssuedDate(List<VoucherReportDto> list, string ClaimNo)
    {
        var voucher = list.Where(i => i.ClaimNumber == ClaimNo)
                          .FirstOrDefault();

        if (voucher != null)
        {
            return voucher.ClosedDate;
        }

        return "";
    }

    private string GetVoucherIssuedBy(List<VoucherReportDto> list, string ClaimNo)
    {
        var voucher = list.Where(i => i.ClaimNumber == ClaimNo)
                          .FirstOrDefault();

        if (voucher != null)
        {
            return voucher.EmployeeName;
        }

        return "";
    }
}
