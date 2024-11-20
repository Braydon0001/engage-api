using Engage.Application.Services.ClaimReports.Models;

namespace Engage.Application.Services.PaymentReports.Commands;

public class GeneratePaymentReportCommand : GetQuery, IRequest<ReportListVM<PaymentReportDto>>
{
    //Required
    public int EngageRegionId { get; set; }
    public int PaymentStatusId { get; set; }
    public int PaymentPeriodId { get; set; }

    //Optional
    public int? CreditorId { get; set; }
    public int? ToPaymentPeriodId { get; set; }
    public int? ExpenseTypeId { get; set; }
    public DateTime? Date { get; set; }
}

public class GeneratePaymentReportCommandHandler : BaseQueryHandler, IRequestHandler<GeneratePaymentReportCommand, ReportListVM<PaymentReportDto>>
{
    public GeneratePaymentReportCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ReportListVM<PaymentReportDto>> Handle(GeneratePaymentReportCommand command, CancellationToken cancellationToken)
    {
        var engageRegion = await _context.EngageRegions
                                            .SingleOrDefaultAsync(e => e.Id == command.EngageRegionId, cancellationToken);

        var paymentPeriod = await _context.PaymentPeriods.Include(c => c.PaymentYear)
                                                         .SingleOrDefaultAsync(c => c.PaymentPeriodId == command.PaymentPeriodId, cancellationToken);

        var query = _context.PaymentLines
                                      .Where(e => e.Payment.PaymentStatusId == command.PaymentStatusId);

        var batchIds = await _context.PaymentBatchRegions.Where(e => e.EngageRegionId == engageRegion.Id)
                                                         .Select(e => e.PaymentBatchId)
                                                         .ToListAsync(cancellationToken);

        query = query.Where(e => batchIds.Contains(e.Payment.PaymentBatchId));

        if (command.CreditorId.HasValue)
        {
            query = query.Where(e => e.Payment.CreditorId == command.CreditorId.Value);
        }

        #region Statuses and Dates
        if (command.PaymentStatusId == (int)PaymentStatusId.New)
        {
            if (command.ToPaymentPeriodId.HasValue)
            {
                var toPaymentPeriod = await _context.PaymentPeriods.Include(c => c.PaymentYear)
                                                                   .SingleOrDefaultAsync(c => c.PaymentPeriodId == command.ToPaymentPeriodId, cancellationToken);

                query = query.Where(e => (e.Payment.Created.Date >= paymentPeriod.StartDate.Date && e.Payment.Created.Date <= toPaymentPeriod.EndDate.Date));
            }
            else
            {
                query = query.Where(e => (e.Payment.Created.Date >= paymentPeriod.StartDate.Date && e.Payment.Created.Date <= paymentPeriod.EndDate.Date));
            }

            if (command.Date.HasValue)
            {
                query = query.Where(e => e.Payment.Created.Date == command.Date.Value.Date);
            }
        }
        else
        {
            if (command.ToPaymentPeriodId.HasValue)
            {
                var toPaymentPeriod = await _context.PaymentPeriods.Include(c => c.PaymentYear)
                                                                   .SingleOrDefaultAsync(c => c.PaymentPeriodId == command.ToPaymentPeriodId, cancellationToken);

                query = query.Where(e => e.Payment.PaymentStatusHistories.Any(h => h.PaymentStatusId == command.PaymentStatusId &&
                                                                                   h.Created.Date >= paymentPeriod.StartDate.Date &&
                                                                                   h.Created.Date <= toPaymentPeriod.EndDate.Date));
            }
            else
            {
                query = query.Where(e => e.Payment.PaymentStatusHistories.Any(h => h.PaymentStatusId == command.PaymentStatusId &&
                                                                                   h.Created.Date >= paymentPeriod.StartDate.Date &&
                                                                                   h.Created.Date <= paymentPeriod.EndDate.Date));
            }

            if (command.Date.HasValue)
            {
                query = query.Where(e => e.Payment.PaymentStatusHistories.Any(h => h.PaymentStatusId == command.PaymentStatusId &&
                                                                                   h.Created.Date == command.Date.Value.Date));
            }
        }
        #endregion

        var entities = await query.ProjectTo<PaymentReportDto>(_mapper.ConfigurationProvider)
                                  .ToListAsync(cancellationToken);

        var creditorTotals = entities.GroupBy(c => c.CreditorName)
                                     .Select(c => new { Amount = c.Sum(b => b.Amount), CreditorName = c.Key })
                                     .OrderByDescending(c => c.Amount)
                                     .ToList();

        var vatTotal = entities.Sum(c => c.VatAmount);
        var reportTotal = entities.Sum(c => c.Amount);

        var Totals = new List<ReportSubTotal>
        {
            new() { Name = "TOTAL VAT", Value = (decimal)vatTotal },
            new() { Name = "TOTAL AMOUNT", Value = (decimal)reportTotal }
        };

        if (creditorTotals.Count > 0)
        {
            foreach (var creditorTotal in creditorTotals)
            {
                Totals.Add(new ReportSubTotal { Name = creditorTotal.CreditorName.ToUpper(), Value = (decimal)creditorTotal.Amount });
            }
        }

        Totals.Add(new ReportSubTotal { Name = "REPORT TOTAL", Value = Math.Round((decimal)reportTotal, 2) });

        string reportName = "Payment Report" + " - " + DateTime.Now.ToString();

        return new ReportListVM<PaymentReportDto>
        {
            Count = entities.Count,
            ReportName = reportName,
            Data = entities,
            ReportSubTotals = Totals,
            RegionName = engageRegion.Name,
        };
    }
}
