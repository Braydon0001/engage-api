namespace Engage.Application.Services.ClaimTypes.Commands;

public class UpdateClaimTypeCommand : ClaimTypeCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateAssetCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateClaimTypeCommand, OperationStatus>
{
    public UpdateAssetCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(UpdateClaimTypeCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.ClaimTypes.SingleAsync(x => x.ClaimTypeId == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.ClaimTypeId;
        return opStatus;
    }
}
