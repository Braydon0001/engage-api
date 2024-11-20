namespace Engage.Application.Services.VoucherDetails.Commands;

public class ProcessVoucherDetailCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
    public bool SaveChanges { get; set; } = true;
}

public class ProcessVoucherDetailCommandHandler : IRequestHandler<ProcessVoucherDetailCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IUserService _user;
    private readonly IDateTimeService _dateTime;

    public ProcessVoucherDetailCommandHandler(IAppDbContext context, IUserService user, IDateTimeService dateTime)
    {
        _context = context;
        _user = user;
        _dateTime = dateTime;
    }

    public async Task<OperationStatus> Handle(ProcessVoucherDetailCommand command, CancellationToken cancellationToken)
    {
        var voucherDetail = await _context.VoucherDetails.SingleAsync(x => x.VoucherDetailId == command.Id, cancellationToken);
        if (voucherDetail == null)
        {
            throw new NotFoundException(nameof(VoucherDetail), command.Id);
        }

        voucherDetail.VoucherDetailStatusId = (int)VoucherDetailStatusId.Issued;
        voucherDetail.ClosedBy = _user.UserName;
        voucherDetail.ClosedDate = DateTime.Now;

        var voucher = await _context.Vouchers.Include(x => x.VoucherDetails)
                                             .SingleAsync(x => x.VoucherId == voucherDetail.VoucherId, cancellationToken);

        if (voucher.VoucherDetails.Count > 1)
        {
            var closedDetails = voucher.VoucherDetails
                                    .Where(x => x.VoucherDetailId != command.Id
                                        && x.VoucherDetailStatusId != (int)VoucherDetailStatusId.Issued
                                        )
                                    .ToList();

            if (!closedDetails.Any())
            {
                voucher.VoucherStatusId = (int)VoucherStatusId.Closed;
            }
        }
        else
        {
            voucher.VoucherStatusId = (int)VoucherStatusId.Closed;
        }

        if (command.SaveChanges)
        {
            var opStatus = await _context.SaveChangesAsync(cancellationToken);
            opStatus.OperationId = command.Id;
            return opStatus;
        }

        return new OperationStatus(status: true);
    }
}