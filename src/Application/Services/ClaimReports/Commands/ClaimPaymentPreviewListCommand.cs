using Engage.Application.Services.ClaimReports.Models;

namespace Engage.Application.Services.ClaimReports.Commands;

public class ClaimPaymentPreviewListCommand : GetQuery, IRequest<ReportListVM<ReportPaymentPreviewDto>>
{
    //Required
    public int ClaimClassificationId { get; set; }
    public List<int> EngageRegionIds { get; set; }
    public int ClaimPeriodId { get; set; }
    public bool IsPaid { get; set; }
    public int? SupplierId { get; set; }
}

public class ClaimPaymentPreviewListCommandHandler : BaseQueryHandler, IRequestHandler<ClaimPaymentPreviewListCommand, ReportListVM<ReportPaymentPreviewDto>>
{
    private readonly ClaimSettings _claimSettings;
    public ClaimPaymentPreviewListCommandHandler(IAppDbContext context, IMapper mapper, IOptions<ClaimSettings> claimSettings) : base(context, mapper)
    {
        _claimSettings = claimSettings.Value;
    }

    public async Task<ReportListVM<ReportPaymentPreviewDto>> Handle(ClaimPaymentPreviewListCommand command, CancellationToken cancellationToken)
    {
        var claimPeriod = await _context.ClaimPeriods.Include(c => c.ClaimYear)
                                                     .SingleOrDefaultAsync(c => c.ClaimPeriodId == command.ClaimPeriodId);

        var payableClaimTypeIDs = await _context.ClaimTypeReportTypes
                                    .Where(c => c.ClaimReportTypeId == _claimSettings.FNBReportTypeId)
                                    .Select(c => c.ClaimTypeId)
                                    .ToListAsync();

        var claimClassification = await _context.ClaimClassifications.SingleOrDefaultAsync(e => e.ClaimClassificationId == command.ClaimClassificationId);

        var query = _context.Claims
                                   .Where(e => e.ClaimClassificationId == command.ClaimClassificationId &&
                                               command.EngageRegionIds.Contains(e.Store.EngageRegionId)
                                               && (e.ApprovedDate.Value.Date >= claimPeriod.StartDate.Date && e.ApprovedDate.Value.Date <= claimPeriod.EndDate.Date)
                                               && e.IsPayStore == true);

        //Exclude standard claims created by certain users as per Matt
        if (claimClassification.ClaimClassificationId == (int)ClaimClassificationId.Standard)
        {
            var excludeList = _claimSettings.ExcludeCreatedBy.ToLower().Split(',').ToList();

            if (excludeList.Count > 0)
            {
                query = query.Where(e => !excludeList.Contains(e.CreatedBy.ToLower()));
            }
        }


        if (command.SupplierId.HasValue)
        {
            query = query.Where(e => e.SupplierId == command.SupplierId);
        }

        if (command.IsPaid)
        {
            query = query.Where(e => (e.ClaimStatusId == (int)ClaimStatusId.Paid));
        }
        else
        {
            query = query.Where(e => e.ClaimStatusId == (int)ClaimStatusId.Approved);
        }

        if (payableClaimTypeIDs.Count > 0)
        {
            query = query.Where(e => payableClaimTypeIDs.Contains(e.ClaimTypeId));
        }

        var entities = await query
                                  .ProjectTo<ReportPaymentPreviewDto>(_mapper.ConfigurationProvider)
                                  .ToListAsync(cancellationToken);

        var data = entities.Select(c => new ReportPaymentPreviewDto
        {
            Id = c.Id,
            StoreId = c.StoreId,
            StoreName = c.StoreName,
            ClaimNumber = c.ClaimNumber,
            TotalClaimNumber = Math.Round(GetClaimStoreTotal(entities, c.StoreId, c.ClaimNumber, c.Created), 2),
            TotalStore = Math.Round(GetStoreTotal(entities, c.StoreId, c.ClaimNumber, c.Created), 2),
            StoreEmailAddress = "",
            BranchNumber = c.BranchNumber,
            AccountNumber = c.AccountNumber,
            AccountType = c.AccountType,
            PaidDate = c.PaidDate,
            PaidBy = c.PaidBy,
        })
                                .OrderBy(c => c.StoreName)
                                .ThenBy(c => c.ClaimNumber)
                                .ThenBy(c => c.Created)
                                .ToList();


        return new ReportListVM<ReportPaymentPreviewDto>
        {
            Data = data,
            Count = data.Count,
            IsPaid = command.IsPaid,
        };
    }

    private decimal GetStoreTotal(List<ReportPaymentPreviewDto> list, int StoreId, string ClaimNumber, DateTime CreatedDate)
    {
        var line = list.Where(i => i.StoreId == StoreId)
                       .OrderByDescending(s => s.Id)
                       .ThenByDescending(s => s.Created)
                       .First();

        if (ClaimNumber == line.ClaimNumber && CreatedDate == line.Created)
        {
            return list.Where(i => i.StoreId == StoreId)
                       .Sum(s => s.TotalAmount);
        }

        return 0;
    }

    private decimal GetClaimStoreTotal(List<ReportPaymentPreviewDto> list, int StoreId, string ClaimNumber, DateTime CreatedDate)
    {
        var date = list.Where(i => i.StoreId == StoreId && i.ClaimNumber == ClaimNumber)
                       .OrderByDescending(s => s.Id)
                       .Select(s => s.Created)
                       .First();

        if (CreatedDate == date)
        {
            return list.Where(i => i.StoreId == StoreId && i.ClaimNumber == ClaimNumber)
                       .Sum(s => s.TotalAmount);
        }

        return 0;
    }

}
