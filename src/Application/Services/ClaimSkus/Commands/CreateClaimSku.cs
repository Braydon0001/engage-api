using EntityFramework.Exceptions.Common;

namespace Engage.Application.Services.ClaimSkus.Commands
{
    public class CreateClaimSkuCommand : ClaimSkuCommand, IRequest<OperationStatus>
    {
        public bool SaveChanges { get; set; } = true;
    }

    public class CreateClaimSkuCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateClaimSkuCommand, OperationStatus>
    {
        public CreateClaimSkuCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<OperationStatus> Handle(CreateClaimSkuCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var entity = _mapper.Map<CreateClaimSkuCommand, ClaimSku>(command);
                _context.ClaimSkus.Add(entity);

                if (command.SaveChanges)
                {
                    var opStatus = await _context.SaveChangesAsync(cancellationToken);
                    opStatus.OperationId = entity.ClaimSkuId;
                    return opStatus;
                }
                else
                {
                    return new OperationStatus
                    {
                        Status = true
                    };
                }

            }
            catch (UniqueConstraintException)
            {
                return OperationStatus.CreateUniqueConstraintException("This product/s has already been added to the Claim. \n It can't be added again.");
            }
        }
    }
}
