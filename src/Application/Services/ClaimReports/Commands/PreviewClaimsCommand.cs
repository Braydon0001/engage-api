using Engage.Application.Services.ClaimReports.Models;

namespace Engage.Application.Services.ClaimReports.Commands;

public class PreviewClaimsCommand : GetQuery, IRequest<ReportListVM<ClaimFNBReport>>
{
    public int[] ClaimIDs { get; set; }
}

public class PreviewClaimsCommandHandler : BaseQueryHandler, IRequestHandler<PreviewClaimsCommand, ReportListVM<ClaimFNBReport>>
{
    private readonly IUserService _user;
    public PreviewClaimsCommandHandler(IAppDbContext context, IMapper mapper, IUserService user) : base(context, mapper)
    {
        _user = user;
    }

    public async Task<ReportListVM<ClaimFNBReport>> Handle(PreviewClaimsCommand command, CancellationToken cancellationToken)
    {
        var query = _context.Claims
                                   .Where(e => command.ClaimIDs.Contains(e.ClaimId));

        var entities = await query.OrderBy(c => c.Store.Name)
                                  .ThenBy(c => c.ClaimNumber)
                                  .ThenBy(c => c.Created)
                                  .ProjectTo<ClaimFNBreportDto>(_mapper.ConfigurationProvider)
                                  .ToListAsync(cancellationToken);



        var data = entities.GroupBy(s => new { s.RecipientName, s.RecipientAccount, s.RecipientAccountType, s.BranchCode, s.OwnReference, s.RecipientReference })
            .Select(c => new ClaimFNBReport
            {
                RecipientName = c.Key.RecipientName,
                RecipientAccount = c.Key.RecipientAccount,
                RecipientAccountType = c.Key.RecipientAccountType,
                BranchCode = c.Key.BranchCode,
                Amount = entities
                            .Where(p => p.RecipientName == c.Key.RecipientName)
                            .Select(t => t.ClaimSkus
                                            .Select(a => a.TotalAmount)
                                            .DefaultIfEmpty()
                                            .Sum())
                            .DefaultIfEmpty()
                            .Sum(),
                OwnReference = c.Key.OwnReference,
                RecipientReference = c.Key.RecipientReference,
            })
            .ToList();

        string reportName = "FNB Report";

        if (command.ClaimIDs.Length > 0)
        {
            var claim = await _context.Claims
                .Include(e => e.ClaimClassification)
                .Include(e => e.Store)
                .Include(e => e.Store.EngageRegion)
                .SingleOrDefaultAsync(e => e.ClaimId == command.ClaimIDs[0], cancellationToken);

            reportName = reportName + " " + claim.ClaimClassification.Name + " " + claim.Store.EngageRegion.Name + " - " + DateTime.Now.ToString();
        }

        string reportAccountNumber = "";
        string reportTitle = "";

        var supplier = await _context.Suppliers.SingleOrDefaultAsync(e => e.SupplierId == _user.SupplierId, cancellationToken);
        if (supplier != null)
        {
            reportAccountNumber = string.IsNullOrEmpty(supplier.ClaimReportAccountNumber) ? "" : supplier.ClaimReportAccountNumber;
            reportTitle = string.IsNullOrEmpty(supplier.ClaimReportAccountNumber) ? "" : supplier.ClaimReportTitle;
        }

        return new ReportListVM<ClaimFNBReport>
        {
            Data = data,
            Count = data.Count,
            ReportName = reportName,
            ReportAccountNumber = reportAccountNumber,
            ReportTitle = reportTitle,
        };
    }
}
