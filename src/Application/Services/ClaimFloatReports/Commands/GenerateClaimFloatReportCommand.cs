using Engage.Application.Services.ClaimFloatReports.Models;
using Engage.Application.Services.ClaimReports.Models;

namespace Engage.Application.Services.ClaimFloatReports.Commands;

public class GenerateClaimFloatReportCommand : GetQuery, IRequest<ReportListVM<ClaimFloatReportDto>>
{
    public int EngageRegionId { get; set; }
    public int ClaimPeriodId { get; set; }
    public int? ToClaimPeriodId { get; set; }
}

public class GenerateClaimFloatReportCommandHandler : BaseQueryHandler, IRequestHandler<GenerateClaimFloatReportCommand, ReportListVM<ClaimFloatReportDto>>
{
    public GenerateClaimFloatReportCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ReportListVM<ClaimFloatReportDto>> Handle(GenerateClaimFloatReportCommand command, CancellationToken cancellationToken)
    {
        var claimPeriod = await _context.ClaimPeriods.Include(c => c.ClaimYear)
                                                     .SingleOrDefaultAsync(c => c.ClaimPeriodId == command.ClaimPeriodId, cancellationToken);

        var region = await _context.EngageRegions.SingleOrDefaultAsync(c => c.Id == command.EngageRegionId, cancellationToken);

        var claimFloats = await _context.ClaimFloats.Include(c => c.ClaimFloatClaims)
                                                        .ThenInclude(x => x.Claim)
                                                            .ThenInclude(x => x.ClaimSkus)
                                                    .Where(c => c.EngageRegionId == command.EngageRegionId)
                                                    .ToListAsync(cancellationToken);

        var claimIds = new List<int>();

        var overallFloatTotals = new List<ReportSubTotal>();
        decimal available = 0;
        if (claimFloats.Any())
        {
            foreach (var claimFloat in claimFloats)
            {
                claimIds.AddRange(claimFloat.ClaimFloatClaims.Select(c => c.ClaimId).ToList());

                var availableAmount = claimFloat.Amount - (claimFloat.ClaimFloatClaims.Select(t => t.Claim.ClaimSkus.Where(s => s.Deleted == false).Select(v => v.TotalAmount).DefaultIfEmpty().Sum()).DefaultIfEmpty().Sum());
                var usedAmount = claimFloat.ClaimFloatClaims.Select(t => t.Claim.ClaimSkus.Where(s => s.Deleted == false).Select(v => v.TotalAmount).DefaultIfEmpty().Sum()).DefaultIfEmpty().Sum();

                overallFloatTotals.Add(new ReportSubTotal
                {
                    Name = claimFloat.Title + " - Overall Used",
                    Value = Math.Round(usedAmount, 2)
                });
                overallFloatTotals.Add(new ReportSubTotal
                {
                    Name = claimFloat.Title + " - Overall Available",
                    Value = Math.Round(availableAmount, 2)
                });
                available += availableAmount;
            }
        }

        var claimQuery = _context.Claims.Where(c => claimIds.Distinct().Contains(c.ClaimId));

        if (command.ToClaimPeriodId.HasValue)
        {
            var toClaimPeriod = await _context.ClaimPeriods.Include(c => c.ClaimYear)
                                                     .SingleOrDefaultAsync(c => c.ClaimPeriodId == command.ToClaimPeriodId);

            claimQuery = claimQuery.Where(e => (e.ApprovedDate.Value.Date >= claimPeriod.StartDate.Date && e.ApprovedDate.Value.Date <= toClaimPeriod.EndDate.Date));
        }
        else
        {
            claimQuery = claimQuery.Where(e => (e.ApprovedDate.Value.Date >= claimPeriod.StartDate.Date && e.ApprovedDate.Value.Date <= claimPeriod.EndDate.Date));
        }

        var data = await claimQuery
                            .ProjectTo<ClaimFloatReportDto>(_mapper.ConfigurationProvider)
                            .ToListAsync();



        var amountUsed = data.Sum(c => c.TotalAmount);
        var floatAmount = claimFloats.Select(t => t.Amount).DefaultIfEmpty().Sum();

        var Totals = new List<ReportSubTotal>
        {
            new ReportSubTotal { Name = "REPORT TOTAL", Value = Math.Round(amountUsed, 2) },
            new ReportSubTotal { Name = "FLOAT AMOUNT", Value = Math.Round(floatAmount, 2) },
            new ReportSubTotal { Name = "OVERALL AVAILABLE", Value = Math.Round(available, 2) }
        };

        Totals.AddRange(overallFloatTotals);

        string reportName = "Claim Float Report - " + DateTime.Now.ToString();

        return new ReportListVM<ClaimFloatReportDto>
        {
            Count = data.Count,
            ReportName = reportName,
            Data = data,
            ReportSubTotals = Totals,
            RegionName = region.Name,
        };
    }
}
