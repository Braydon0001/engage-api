namespace Engage.Application.Services.StoreBankDetails.Commands;

public class UpdateStoreBankDetailCommand : StoreBankDetailCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateBankDetailCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateStoreBankDetailCommand, OperationStatus>
{
    public UpdateBankDetailCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(UpdateStoreBankDetailCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.StoreBankDetails.SingleAsync(x => x.StoreBankDetailId == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.StoreBankDetailId;
        return opStatus;
    }
}
