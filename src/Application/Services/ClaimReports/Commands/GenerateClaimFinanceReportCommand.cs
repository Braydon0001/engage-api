using Engage.Application.Services.ClaimReports.Models;

namespace Engage.Application.Services.ClaimReports.Commands;

public class GenerateClaimFinanceReportCommand : GetQuery, IRequest<ReportListVM<ClaimReportDto>>
{
    //Required
    public List<int> ClaimClassificationIds { get; set; }
    public int EngageRegionId { get; set; }
    public int ClaimPeriodId { get; set; }

    //Optional
    public int? SupplierId { get; set; }
    public int? ToClaimPeriodId { get; set; }
    public List<int> ClaimTypeIds { get; set; }
    public int? StoreId { get; set; }
    public int? ClaimAccountManagerId { get; set; }
    public int? ClaimManagerId { get; set; }
    public int? ProductSupplierId { get; set; }
}

public class GenerateClaimFinanceReportCommandHandler : BaseQueryHandler, IRequestHandler<GenerateClaimFinanceReportCommand, ReportListVM<ClaimReportDto>>
{
    public GenerateClaimFinanceReportCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ReportListVM<ClaimReportDto>> Handle(GenerateClaimFinanceReportCommand command, CancellationToken cancellationToken)
    {
        var engageRegion = await _context.EngageRegions
                                            .SingleOrDefaultAsync(e => e.Id == command.EngageRegionId, cancellationToken);

        var claimPeriod = await _context.ClaimPeriods.Include(c => c.ClaimYear)
                                                     .SingleOrDefaultAsync(c => c.ClaimPeriodId == command.ClaimPeriodId, cancellationToken);

        var query = _context.ClaimSkus
                                      .Where(e => command.ClaimClassificationIds.Contains(e.Claim.ClaimClassificationId) &&
                                                  e.Claim.Store.EngageRegionId == command.EngageRegionId
                                                  && e.Claim.IsPayStore == true
                                                  && (e.Claim.ClaimStatusId == (int)ClaimStatusId.Approved ||
                                                      e.Claim.ClaimStatusId == (int)ClaimStatusId.Paid));

        if (command.ToClaimPeriodId.HasValue)
        {
            var toClaimPeriod = await _context.ClaimPeriods.Include(c => c.ClaimYear)
                                                     .SingleOrDefaultAsync(c => c.ClaimPeriodId == command.ToClaimPeriodId);

            query = query.Where(e => (e.Claim.ApprovedDate.Value.Date >= claimPeriod.StartDate.Date && e.Claim.ApprovedDate.Value.Date <= toClaimPeriod.EndDate.Date));
        }
        else
        {
            query = query.Where(e => (e.Claim.ApprovedDate.Value.Date >= claimPeriod.StartDate.Date && e.Claim.ApprovedDate.Value.Date <= claimPeriod.EndDate.Date));
        }


        if (command.SupplierId.HasValue)
        {
            query = query.Where(e => e.Claim.SupplierId == command.SupplierId);
        }

        if (command.ClaimTypeIds?.Any() == true)
        {
            query = query.Where(e => command.ClaimTypeIds.Contains(e.Claim.ClaimTypeId));
        }

        if (command.StoreId.HasValue)
        {
            query = query.Where(e => e.Claim.StoreId == command.StoreId);
        }

        if (command.ClaimAccountManagerId.HasValue)
        {
            query = query.Where(e => e.Claim.ClaimAccountManagerId == command.ClaimAccountManagerId);
        }

        if (command.ClaimManagerId.HasValue)
        {
            query = query.Where(e => e.Claim.ClaimManagerId == command.ClaimManagerId);
        }

        var entities = await query
                                  .ProjectTo<ClaimReportDto>(_mapper.ConfigurationProvider)
                                  .ToListAsync(cancellationToken);

        if (command.ProductSupplierId.HasValue)
        {
            entities = entities.Where(e => e.ProductSupplierId == command.ProductSupplierId).ToList();
        }

        var data = entities.Select(c => new ClaimReportDto
        {
            StoreId = c.StoreId,
            StoreName = c.StoreName,
            ClaimNumber = c.ClaimNumber,
            ClaimReference = c.ClaimReference,
            Quantity = c.Quantity,
            CreatedDate = c.CreatedDate,
            ClaimDate = c.ClaimDate,
            FallsWithinPeriod = ClaimDateFallsWithinPeriod(claimPeriod.StartDate, claimPeriod.EndDate, c.ClaimDate) ? "YES" : "NO",
            ProductName = c.ProductName,
            ClaimSupplierName = c.ClaimSupplierName,
            ProductSupplierName = c.ProductSupplierName,
            ProductSupplierId = c.ProductSupplierId,
            ClaimTypeName = c.ClaimTypeName,
            ClaimAccountManagerName = c.ClaimAccountManagerName,
            ClaimManagerName = c.ClaimManagerName,
            IsPayStore = c.IsPayStore,
            IsClaimFromSupplier = c.IsClaimFromSupplier,
            ProductCreatedDate = c.ProductCreatedDate,
            Amount = c.Amount,
            TotalAmount = c.TotalAmount,
            StoreClaimNumberTotal = GetClaimStoreTotal(entities, c.StoreId, c.ClaimId, c.ProductCreatedDate),
            StoreTotal = GetStoreTotal(entities, c.StoreId, c.ClaimId, c.ProductCreatedDate),
            Note = c.Note,
            CreatedBy = c.CreatedBy,
            VatAmount = c.VatAmount,
            ClaimId = c.ClaimId,
            IsMaasProduct = c.IsMaasProduct,
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
        var maasTotal = data.Where(c => c.IsMaasProduct == "YES").Sum(c => c.TotalAmount);

        var Totals = new List<ReportSubTotal>();
        Totals.Add(new ReportSubTotal { Name = "TOTAL EXCL. VAT", Value = totalExcVat });
        Totals.Add(new ReportSubTotal { Name = "TOTAL VAT", Value = vatTotal });
        Totals.Add(new ReportSubTotal { Name = "MAAS AMOUNT", Value = maasTotal });

        if (claimTypeTotals.Count > 0)
        {
            foreach (var typeTotal in claimTypeTotals)
            {
                Totals.Add(new ReportSubTotal { Name = typeTotal.ClaimTypeName.ToUpper(), Value = typeTotal.Amount });
            }
        }

        Totals.Add(new ReportSubTotal { Name = "REPORT TOTAL", Value = Math.Round(reportTotal, 2) });

        string reportName = "Finance Report" + " - " + DateTime.Now.ToString();

        return new ReportListVM<ClaimReportDto>
        {
            Count = data.Count,
            ReportName = reportName,
            Data = data,
            ReportSubTotals = Totals,
            RegionName = engageRegion.Name,
        };
    }

    private decimal GetStoreTotal(List<ClaimReportDto> list, int StoreId, int ClaimId, DateTime CreatedDate)
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

    private decimal GetClaimStoreTotal(List<ClaimReportDto> list, int StoreId, int ClaimId, DateTime CreatedDate)
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

    private bool ClaimDateFallsWithinPeriod(DateTime StartDate, DateTime EndDate, string ClaimDate)
    {
        try
        {
            var date = DateTime.Parse(ClaimDate);

            return date.Date >= StartDate.Date && date.Date <= EndDate.Date;

        }
        catch (Exception)
        {
            return false;
        }
    }
}
