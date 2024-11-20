namespace Engage.Application.Services.ClaimSkuTypes.Commands;

public class UpdateClaimSkuTypeCommand : ClaimSkuTypeCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateAssetCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateClaimSkuTypeCommand, OperationStatus>
{
    public UpdateAssetCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(UpdateClaimSkuTypeCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.ClaimSkuTypes.SingleAsync(x => x.ClaimSkuTypeId == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.ClaimSkuTypeId;
        return opStatus;
    }
}
