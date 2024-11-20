namespace Engage.Application.Services.Vat.Commands;

public class UpdateVatCommand : VatCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateVatCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateVatCommand, OperationStatus>
{
    public UpdateVatCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(UpdateVatCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Vat.SingleAsync(x => x.VatId == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.VatId;
        return opStatus;
    }
}
