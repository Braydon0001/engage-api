using Engage.Application.Services.ClaimReports.Models;

namespace Engage.Application.Services.ClaimReports.Commands;

public class GenerateClaimReportCommand : GetQuery, IRequest<ReportListVM<ClaimReportDto>>
{
    //Required
    public int ClaimClassificationId { get; set; }
    public List<int> EngageRegionIds { get; set; }
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

public class GetClaimReportQueryHandler : BaseQueryHandler, IRequestHandler<GenerateClaimReportCommand, ReportListVM<ClaimReportDto>>
{
    public GetClaimReportQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ReportListVM<ClaimReportDto>> Handle(GenerateClaimReportCommand command, CancellationToken cancellationToken)
    {
        var engageRegions = await _context.EngageRegions.Where(e => command.EngageRegionIds.Contains(e.Id))
                                                         .ToListAsync(cancellationToken);

        var claimPeriod = await _context.ClaimPeriods.Include(c => c.ClaimYear)
                                                     .SingleOrDefaultAsync(c => c.ClaimPeriodId == command.ClaimPeriodId, cancellationToken);

        var claimClassification = await _context.ClaimClassifications.SingleOrDefaultAsync(e => e.ClaimClassificationId == command.ClaimClassificationId, cancellationToken);

        var query = _context.ClaimSkus
                                      .Where(e => e.Claim.ClaimClassificationId == command.ClaimClassificationId &&
                                                  command.EngageRegionIds.Contains(e.Claim.Store.EngageRegionId)
                                                  && (e.Claim.ClaimStatusId == (int)ClaimStatusId.Approved ||
                                                      e.Claim.ClaimStatusId == (int)ClaimStatusId.Paid));

        string reportName = claimClassification.Name;

        if (engageRegions.Count > 0)
        {
            foreach (var engageRegion in engageRegions)
            {
                reportName = reportName + " - " + engageRegion.Name;
            }
        }

        if (command.ToClaimPeriodId.HasValue)
        {
            var toClaimPeriod = await _context.ClaimPeriods.Include(c => c.ClaimYear)
                                                     .SingleOrDefaultAsync(c => c.ClaimPeriodId == command.ToClaimPeriodId);

            reportName = reportName + " " + claimPeriod.ClaimYear.Name + "-" + claimPeriod.Name + " to " + toClaimPeriod.ClaimYear.Name + "-" + toClaimPeriod.Name;

            query = query.Where(e => (e.Claim.ApprovedDate.Value.Date >= claimPeriod.StartDate.Date && e.Claim.ApprovedDate.Value.Date <= toClaimPeriod.EndDate.Date));
        }
        else
        {
            reportName = reportName + " " + claimPeriod.ClaimYear.Name + "-" + claimPeriod.Name;
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
            Amount = Math.Round(c.Amount, 2),
            TotalAmount = Math.Round(c.TotalAmount, 2),
            StoreClaimNumberTotal = Math.Round(GetClaimStoreTotal(entities, c.StoreId, c.ClaimId, c.ProductCreatedDate), 2),
            StoreTotal = Math.Round(GetStoreTotal(entities, c.StoreId, c.ClaimId, c.ProductCreatedDate), 2),
            Note = c.Note,
            CreatedBy = c.CreatedBy,
            VatAmount = Math.Round(c.VatAmount, 2),
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

        var vatTotal = Math.Round(data.Sum(c => c.VatAmount), 2);
        var reportTotal = Math.Round(data.Sum(c => c.TotalAmount), 2);
        var totalExcVat = reportTotal - vatTotal;
        var maasTotal = data.Where(c => c.IsMaasProduct == "YES").Sum(c => c.TotalAmount);

        var Totals = new List<ReportSubTotal>();
        Totals.Add(new ReportSubTotal { Name = "TOTAL EXCL. VAT", Value = totalExcVat });
        Totals.Add(new ReportSubTotal { Name = "TOTAL VAT", Value = vatTotal });
        Totals.Add(new ReportSubTotal { Name = "MAAS AMOUNT", Value = Math.Round(maasTotal, 2) });

        if (claimTypeTotals.Count > 0)
        {
            foreach (var typeTotal in claimTypeTotals)
            {
                Totals.Add(new ReportSubTotal { Name = typeTotal.ClaimTypeName.ToUpper(), Value = typeTotal.Amount });
            }
        }

        Totals.Add(new ReportSubTotal { Name = "REPORT TOTAL", Value = reportTotal });

        reportName = reportName + " - " + DateTime.Now.ToString();

        return new ReportListVM<ClaimReportDto>
        {
            Count = data.Count,
            ReportName = reportName,
            Data = data,
            ReportSubTotals = Totals,
            ClassificationName= claimClassification.Name,
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
}
