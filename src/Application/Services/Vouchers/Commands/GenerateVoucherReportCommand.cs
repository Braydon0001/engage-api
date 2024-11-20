using Engage.Application.Services.Vouchers.Models;

namespace Engage.Application.Services.Vouchers.Commands;

public class GenerateVoucherReportCommand : GetQuery, IRequest<VoucherReportVM<VoucherReportDto>>
{
    public List<int> EngageRegionIds { get; set; }
    public int ClaimPeriodId { get; set; }
    public VoucherDetailStatusId StatusNumber { get; set; } = VoucherDetailStatusId.Received;
}
public class GenerateVoucherReportCommandHandler : BaseQueryHandler, IRequestHandler<GenerateVoucherReportCommand, VoucherReportVM<VoucherReportDto>>
{
    public GenerateVoucherReportCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {

    }
    public async Task<VoucherReportVM<VoucherReportDto>> Handle(GenerateVoucherReportCommand command, CancellationToken cancellationToken)
    {
        var query = _context.VoucherDetails.AsQueryable().AsNoTracking();

        var claimPeriod = await _context.ClaimPeriods.Include(c => c.ClaimYear)
                                                     .SingleOrDefaultAsync(c => c.ClaimPeriodId == command.ClaimPeriodId, cancellationToken);


        if (command.EngageRegionIds != null && command.EngageRegionIds.Any())
        {
            query = query.Where(e => command.EngageRegionIds.Contains(e.Voucher.EngageRegionId)).AsQueryable();
        }

        switch (command.StatusNumber)
        {
            case VoucherDetailStatusId.Received:
                query = query.Where(e => e.VoucherDetailStatusId == (int)VoucherDetailStatusId.Received
                    && e.Created.Date >= claimPeriod.StartDate.Date && e.Created.Date <= claimPeriod.EndDate.Date)
                    /*.Include(e => e.Voucher)*/;
                break;
            case VoucherDetailStatusId.Assigned:
                query = query.Where(e => e.VoucherDetailStatusId == (int)VoucherDetailStatusId.Assigned
                    && e.AssignedDate.Value.Date >= claimPeriod.StartDate.Date && e.AssignedDate.Value.Date <= claimPeriod.EndDate.Date)
                    /*.Include(e => e.Voucher)*/;
                break;
            case VoucherDetailStatusId.Issued:
                query = query.Where(e => e.VoucherDetailStatusId == (int)VoucherDetailStatusId.Issued
                    //&& e.Claim.ClaimPeriodId == command.ClaimPeriodId
                    && e.Claim.ApprovedDate.Value.Date >= claimPeriod.StartDate.Date && e.Claim.ApprovedDate.Value.Date <= claimPeriod.EndDate.Date)
                    /*.Include(e => e.Voucher)*/;
                //TODO fetch store name and claim number?
                break;
        }

        var data = await query
            .OrderBy(c => c.VoucherDetailId)
            .Include(e => e.Voucher)
            .ProjectTo<VoucherReportDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        if (command.StatusNumber == VoucherDetailStatusId.Issued)
        {
            foreach (var detail in data)
            {
                if (detail.Files != null)
                {
                    detail.AttachmentUrl = String.Join(" , ", detail.Files.Select(e => e.Url));
                    detail.Files = null;
                }
            }
        }

        List<string> columnNames = new List<string>();
        columnNames.Add("Campaign Id");                     // A
        columnNames.Add("Campaign Name");                   // B
        columnNames.Add("Campaign Total Amount");           // C
        columnNames.Add("Campaign Total Amount Used");      // D
        columnNames.Add("Campaign Note");                   // E
        columnNames.Add("Voucher Id");                      // F
        columnNames.Add("Voucher Number");                  // G
        columnNames.Add("Note");                            // H
        columnNames.Add("Amount");                          // I
        columnNames.Add("Recieved Date");                   // J
        if (command.StatusNumber != VoucherDetailStatusId.Received)
        {
            columnNames.Add("Employee Code");               // K on assigned and Issued
            columnNames.Add("Employee Name");               // L on assigned and Issued
            columnNames.Add("Assigned Date");               // M on assigned and Issued
            columnNames.Add("Claim Number");                // N on assigned and Issued
            columnNames.Add("Store Name");                  // O on assigned and Issued
        }
        if (command.StatusNumber == VoucherDetailStatusId.Issued)
        {
            columnNames.Add("Store Contact");               // P on Issued
            columnNames.Add("Closed Date");                 // Q on Issued
            columnNames.Add("Image Url");                   // R on Issued
        }

        return new VoucherReportVM<VoucherReportDto>
        {
            Count = data.Count(),
            ReportName = "Voucher Reports " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
            ColumnNames = columnNames,
            Data = data
        };
    }
}
