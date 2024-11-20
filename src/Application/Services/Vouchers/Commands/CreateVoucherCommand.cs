namespace Engage.Application.Services.Vouchers.Commands;

public class CreateVoucherCommand : VoucherCommand, IRequest<OperationStatus>
{
}

public class CreateVoucherCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateVoucherCommand, OperationStatus>
{

    public CreateVoucherCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator)
    {

    }

    public async Task<OperationStatus> Handle(CreateVoucherCommand command, CancellationToken cancellationToken)
    {
        var Voucher = _mapper.Map<CreateVoucherCommand, Voucher>(command);

        Voucher.VoucherStatusId = (int)VoucherStatusId.Open;

        _context.Vouchers.Add(Voucher);
        var opStatus = await _context.SaveChangesAsync(cancellationToken);

        opStatus.OperationId = Voucher.VoucherId;
        return opStatus;

    }
}
