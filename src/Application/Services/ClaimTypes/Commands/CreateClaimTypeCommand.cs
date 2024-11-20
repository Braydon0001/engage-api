namespace Engage.Application.Services.ClaimTypes.Commands;

public class CreateClaimTypeCommand : ClaimTypeCommand, IRequest<OperationStatus>
{
}

public class CreateClaimTypeCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateClaimTypeCommand, OperationStatus>
{
    public CreateClaimTypeCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(CreateClaimTypeCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateClaimTypeCommand, ClaimType>(command);
        _context.ClaimTypes.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.ClaimTypeId;
        return opStatus;
    }
}
