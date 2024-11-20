using EntityFramework.Exceptions.Common;

namespace Engage.Application.Services.ClaimSkus.Commands
{
    public class CreateClaimSkusCommand : IRequest<OperationStatus>
    {
        public int ClaimId { get; set; }
        public List<int> DcProductIds { get; set; }
    }

    public class CreateClaimSkusCommandHandler : IRequestHandler<CreateClaimSkusCommand, OperationStatus>
    {
        private readonly IAppDbContext _context;
        private readonly IMediator _mediator;

        public CreateClaimSkusCommandHandler(IAppDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<OperationStatus> Handle(CreateClaimSkusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                foreach (var id in request.DcProductIds)
                {
                    var existingCheck = await _context.ClaimSkus
                                    .Where(c => c.ClaimId == request.ClaimId
                                           && c.DCProductId == id)
                                    .FirstOrDefaultAsync();

                    if (existingCheck != null)
                    {
                        throw new ClaimException("This Claim already has at least one of the selected Products. \n\n It can't be added again.");
                    }

                    await _mediator.Send(new CreateClaimSkuCommand
                    {
                        ClaimSkuStatusId = (int)ClaimSkuStatusId.Default,
                        ClaimSkuTypeId = 1,
                        ClaimQuantityTypeId = 1,
                        ClaimId = request.ClaimId,
                        DCProductId = id,
                        Quantity = 0,
                        Amount = 0,
                        VatAmount = 0
                    }); ;
                }

                var operationStatus = await _context.SaveChangesAsync(cancellationToken);
                operationStatus.OperationId = request.ClaimId;
                return operationStatus;
            }
            catch (UniqueConstraintException)
            {
                return OperationStatus.CreateUniqueConstraintException("This product/s has already been added to the Claim. \n\n It can't be added again.");                
            }
        }
    }
}
