namespace Engage.Application.Services.Vat.Commands;

public class CreateVatCommand : VatCommand, IRequest<OperationStatus>
{
}

public class CreateVatCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateVatCommand, OperationStatus>
{
    public CreateVatCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(CreateVatCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateVatCommand, Domain.Entities.Vat>(command);
        _context.Vat.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.VatId;
        return opStatus;
    }
}
