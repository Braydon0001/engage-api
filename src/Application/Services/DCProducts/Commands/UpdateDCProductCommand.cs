namespace Engage.Application.Services.DCProducts.Commands;

public class UpdateDCProductCommand : DCProductCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateDCProductCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateDCProductCommand, OperationStatus>
{
    public UpdateDCProductCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(UpdateDCProductCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.DCProducts.SingleAsync(x => x.DCProductId == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.DCProductId;
        return opStatus;
    }
}
