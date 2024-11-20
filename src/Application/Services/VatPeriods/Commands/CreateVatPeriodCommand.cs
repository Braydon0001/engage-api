namespace Engage.Application.Services.VatPeriods.Commands;

public class CreateVatPeriodCommand : VatPeriodCommand, IRequest<OperationStatus>
{
}

public class CreateVatPeriodCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateVatPeriodCommand, OperationStatus>
{
    public CreateVatPeriodCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(CreateVatPeriodCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateVatPeriodCommand, VatPeriod>(command);
        _context.VatPeriods.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.VatPeriodId;
        return opStatus;
    }
}
