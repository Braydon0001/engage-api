namespace Engage.Application.Services.Vouchers.Commands;

public class UpdateVoucherCommand : VoucherCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateVoucherCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateVoucherCommand, OperationStatus>
{
    private readonly IUserService _user;

    public UpdateVoucherCommandHandler(IAppDbContext context, IMapper mapper, IUserService user) : base(context, mapper)
    {
        _user = user;
    }

    public async Task<OperationStatus> Handle(UpdateVoucherCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Vouchers
                .FirstOrDefaultAsync(x => x.VoucherId == command.Id);

        if (entity == null)
            throw new NotFoundException(nameof(Voucher), command.Id);

        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = command.Id;
        return opStatus;
    }
}
