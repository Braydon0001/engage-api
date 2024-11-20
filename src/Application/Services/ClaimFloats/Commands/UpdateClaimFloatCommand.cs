namespace Engage.Application.Services.ClaimFloats.Commands;

public class UpdateClaimFloatCommand : ClaimFloatCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateClaimFloatCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateClaimFloatCommand, OperationStatus>
{
    public UpdateClaimFloatCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(UpdateClaimFloatCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.ClaimFloats.SingleOrDefaultAsync(x => x.ClaimFloatId == request.Id);
        _mapper.Map(request, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.ClaimFloatId;
        return opStatus;
    }
}
