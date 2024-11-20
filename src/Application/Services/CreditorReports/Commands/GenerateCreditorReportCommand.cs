using Engage.Application.Services.ClaimReports.Models;

namespace Engage.Application.Services.CreditorReports.Commands;

public class GenerateCreditorReportCommand : GetQuery, IRequest<ReportListVM<CreditorReportDto>>
{
    //Required
    public int CreditorStatusId { get; set; }
    public int PaymentPeriodId { get; set; }

    //Optional
    public int? ToPaymentPeriodId { get; set; }
    public DateTime? Date { get; set; }
}

public class GenerateCreditorReportCommandHandler : BaseQueryHandler, IRequestHandler<GenerateCreditorReportCommand, ReportListVM<CreditorReportDto>>
{
    public GenerateCreditorReportCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ReportListVM<CreditorReportDto>> Handle(GenerateCreditorReportCommand command, CancellationToken cancellationToken)
    {
        var paymentPeriod = await _context.PaymentPeriods.Include(c => c.PaymentYear)
                                                         .SingleOrDefaultAsync(c => c.PaymentPeriodId == command.PaymentPeriodId, cancellationToken);

        var query = _context.Creditors.Where(e => e.CreditorStatusId == command.CreditorStatusId);

        #region Statuses and Dates
        if (command.CreditorStatusId == (int)CreditorStatusId.New)
        {
            if (command.ToPaymentPeriodId.HasValue)
            {
                var toPaymentPeriod = await _context.PaymentPeriods.Include(c => c.PaymentYear)
                                                                   .SingleOrDefaultAsync(c => c.PaymentPeriodId == command.ToPaymentPeriodId, cancellationToken);

                query = query.Where(e => (e.Created.Date >= paymentPeriod.StartDate.Date && e.Created.Date <= toPaymentPeriod.EndDate.Date));
            }
            else
            {
                query = query.Where(e => (e.Created.Date >= paymentPeriod.StartDate.Date && e.Created.Date <= paymentPeriod.EndDate.Date));
            }

            if (command.Date.HasValue)
            {
                query = query.Where(e => e.Created.Date == command.Date.Value.Date);
            }
        }
        else
        {
            if (command.ToPaymentPeriodId.HasValue)
            {
                var toPaymentPeriod = await _context.PaymentPeriods.Include(c => c.PaymentYear)
                                                                   .SingleOrDefaultAsync(c => c.PaymentPeriodId == command.ToPaymentPeriodId, cancellationToken);

                query = query.Where(e => e.CreditorStatusHistories.Any(h => h.CreditorStatusId == command.CreditorStatusId &&
                                                                            h.Created.Date >= paymentPeriod.StartDate.Date &&
                                                                            h.Created.Date <= toPaymentPeriod.EndDate.Date));
            }
            else
            {
                query = query.Where(e => e.CreditorStatusHistories.Any(h => h.CreditorStatusId == command.CreditorStatusId &&
                                                                            h.Created.Date >= paymentPeriod.StartDate.Date &&
                                                                            h.Created.Date <= paymentPeriod.EndDate.Date));
            }

            if (command.Date.HasValue)
            {
                query = query.Where(e => e.CreditorStatusHistories.Any(h => h.CreditorStatusId == command.CreditorStatusId &&
                                                                            h.Created.Date == command.Date.Value.Date));
            }
        }
        #endregion

        var entities = await query.ProjectTo<CreditorReportDto>(_mapper.ConfigurationProvider)
                                  .ToListAsync(cancellationToken);

        string reportName = "Creditor Report" + " - " + DateTime.Now.ToString();

        return new ReportListVM<CreditorReportDto>
        {
            Count = entities.Count,
            ReportName = reportName,
            Data = entities,
        };
    }
}
