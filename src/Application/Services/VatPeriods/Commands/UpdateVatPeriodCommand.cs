namespace Engage.Application.Services.VatPeriods.Commands;

public class UpdateVatPeriodCommand : VatPeriodCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateVatPeriodCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateVatPeriodCommand, OperationStatus>
{
    public UpdateVatPeriodCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(UpdateVatPeriodCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.VatPeriods.SingleAsync(x => x.VatPeriodId == command.Id);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.VatPeriodId;
        return opStatus;
    }
}
