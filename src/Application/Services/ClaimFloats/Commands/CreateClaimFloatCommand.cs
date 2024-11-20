namespace Engage.Application.Services.ClaimFloats.Commands;

public class CreateClaimFloatCommand : ClaimFloatCommand, IRequest<OperationStatus>
{
}

public class CreateClaimFloatCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateClaimFloatCommand, OperationStatus>
{
    public CreateClaimFloatCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(CreateClaimFloatCommand command, CancellationToken cancellationToken)
    {
        //var existingClaimFloat = await _context.ClaimFloats
        //                                        .Where(c => c.SupplierId == command.SupplierId
        //                                            && c.EngageRegionId == command.EngageRegionId
        //                                            && c.Disabled == false
        //                                            && c.Deleted == false)
        //                                        .FirstOrDefaultAsync();

        //if (existingClaimFloat != null)
        //{
        //    throw new ClaimException("This region already has a Claim Float for this supplier. \n\n It can't be added again. \n You can Topup the existing one or Ask an Administrator to Disable it.");
        //}

        var entity = _mapper.Map<CreateClaimFloatCommand, ClaimFloat>(command);

        _context.ClaimFloats.Add(entity);

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = entity.ClaimFloatId;
        return operationStatus;

    }
}
