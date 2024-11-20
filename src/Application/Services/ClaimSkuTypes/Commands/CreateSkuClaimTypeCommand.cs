namespace Engage.Application.Services.ClaimSkuTypes.Commands;

public class CreateClaimSkuTypeCommand : ClaimSkuTypeCommand, IRequest<OperationStatus>
{
}

public class CreateSkuClaimTypeCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateClaimSkuTypeCommand, OperationStatus>
{
    public CreateSkuClaimTypeCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(CreateClaimSkuTypeCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateClaimSkuTypeCommand, ClaimSkuType>(command);
        _context.ClaimSkuTypes.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.ClaimSkuTypeId;
        return opStatus;
    }
}
