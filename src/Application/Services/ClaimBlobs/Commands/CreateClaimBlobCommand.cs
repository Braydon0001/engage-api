using Engage.Application.Services.EntityBlobs.Commands;

namespace Engage.Application.Services.ClaimBlobs.Commands;

public class CreateClaimBlobCommand : EntityBlobCommand, IRequest<OperationStatus>, IMapTo<ClaimBlob>
{
    public int ClaimId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateClaimBlobCommand, ClaimBlob>();
    }
}

public class CreateBlobCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateClaimBlobCommand, OperationStatus>
{
    public CreateBlobCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(CreateClaimBlobCommand command, CancellationToken cancellationToken)
    {
        var blob = await _context.ClaimBlobs.FirstOrDefaultAsync(e => e.ClaimId == command.ClaimId &&
                                                                      e.FileName == command.FileName, cancellationToken);
        if (blob != null)
        {
            return new OperationStatus
            {
                Status = true,
                OperationId = blob.EntityBlobId,
            };
        }

        var entity = _mapper.Map<CreateClaimBlobCommand, ClaimBlob>(command);
        _context.ClaimBlobs.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EntityBlobId;
        return opStatus;
    }
}
