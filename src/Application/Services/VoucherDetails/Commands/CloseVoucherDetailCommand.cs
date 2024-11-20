namespace Engage.Application.Services.VoucherDetails.Commands;

public class CloseVoucherDetailCommand : VoucherDetailCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
    public bool SaveChanges { get; set; } = true;
}

public class CloseVoucherDetailCommandHandler : IRequestHandler<CloseVoucherDetailCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IUserService _user;
    private readonly IDateTimeService _dateTime;

    public CloseVoucherDetailCommandHandler(IAppDbContext context, IUserService user, IDateTimeService dateTime)
    {
        _context = context;
        _user = user;
        _dateTime = dateTime;
    }

    public async Task<OperationStatus> Handle(CloseVoucherDetailCommand command, CancellationToken cancellationToken)
    {
        var voucherDetail = await _context.VoucherDetails.SingleAsync(x => x.VoucherDetailId == command.Id, cancellationToken);
        if (voucherDetail == null)
        {
            throw new NotFoundException(nameof(VoucherDetail), command.Id);
        }
        var claim = await _context.Claims.SingleAsync(x => x.ClaimId == command.ClaimId, cancellationToken);
        if (claim == null)
        {
            throw new NotFoundException(nameof(Claim), command.Id);
        }

        voucherDetail.VoucherDetailStatusId = (int)VoucherDetailStatusId.Issued;
        voucherDetail.ClosedBy = _user.UserName;
        voucherDetail.ClosedDate = _dateTime.Now;
        voucherDetail.ClaimId = command.ClaimId;
        voucherDetail.StoreId = claim.StoreId;

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
                voucher.ClosedDate = _dateTime.Now;
                voucher.ClosedBy = _user.UserName;
            }
        }
        else
        {
            voucher.VoucherStatusId = (int)VoucherStatusId.Closed;
            voucher.ClosedDate = _dateTime.Now;
            voucher.ClosedBy = _user.UserName;
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