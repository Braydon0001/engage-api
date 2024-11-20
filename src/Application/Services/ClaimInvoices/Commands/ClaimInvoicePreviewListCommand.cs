using Engage.Application.Services.ClaimReports.Models;

namespace Engage.Application.Services.ClaimInvoices.Commands
{
    public class ClaimInvoicePreviewListCommand : GetQuery, IRequest<ReportListVM<ReportPaymentPreviewDto>>
    {
        public List<int> ClaimClassificationIds { get; set; }
        public List<int> EngageRegionIds { get; set; }
        public int ClaimPeriodId { get; set; }
        public bool IsExcludePayStoreIsNo { get; set; }
        public bool IsPaid { get; set; }
        public int? SupplierId { get; set; }
        public int? ClaimAccountManagerId { get; set; }
    }
    public class ClaimInvoicePreviewListCommandHandler : BaseQueryHandler, IRequestHandler<ClaimInvoicePreviewListCommand, ReportListVM<ReportPaymentPreviewDto>>
    {
        private readonly ClaimSettings _claimSettings;
        public ClaimInvoicePreviewListCommandHandler(IAppDbContext context, IMapper mapper, IOptions<ClaimSettings> claimSettings) : base(context, mapper)
        {
            _claimSettings = claimSettings.Value;
        }

        public async Task<ReportListVM<ReportPaymentPreviewDto>> Handle(ClaimInvoicePreviewListCommand command, CancellationToken cancellationToken)
        {
            var claimPeriod = await _context.ClaimPeriods.Include(c => c.ClaimYear)
                                                         .SingleOrDefaultAsync(c => c.ClaimPeriodId == command.ClaimPeriodId);

            var query = _context.Claims.Include(e => e.Store)
                                       .Include(e => e.Store.BankDetails)
                                       .Include(e => e.Store.StoreContacts)
                                       .Include(e => e.ClaimSkus)
                                       .Where(e => command.ClaimClassificationIds.Contains(e.ClaimClassificationId) &&
                                                   command.EngageRegionIds.Contains(e.Store.EngageRegionId)
                                                   && (e.ApprovedDate.Value.Date >= claimPeriod.StartDate.Date && e.ApprovedDate.Value.Date <= claimPeriod.EndDate.Date));

            if (command.SupplierId.HasValue)
            {
                query = query.Where(e => e.SupplierId == command.SupplierId);
            }

            if (command.ClaimAccountManagerId.HasValue)
            {
                query = query.Where(e => e.ClaimAccountManagerId == command.ClaimAccountManagerId);
            }

            if (command.IsExcludePayStoreIsNo)
            {
                query = query.Where(e => e.IsPayStore == true);
            }

            if (command.IsPaid)
            {
                query = query.Where(e => (e.ClaimStatusId == (int)ClaimStatusId.Paid));
            }
            else
            {
                query = query.Where(e => e.ClaimStatusId == (int)ClaimStatusId.Approved);
            }

            var entities = await query.OrderBy(c => c.Store.Name)
                                      .ThenBy(c => c.ClaimNumber)
                                      .ThenBy(c => c.Created)
                                      .ProjectTo<ReportPaymentPreviewDto>(_mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

            var data = entities
                .Select(c => new ReportPaymentPreviewDto
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
}