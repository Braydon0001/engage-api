using Engage.Application.Services.ClaimHistories.Commands;
using Engage.Application.Services.DistributionCenters.Queries;

namespace Engage.Application.Services.Claims.Commands;

public class CreateClaimCommand : ClaimCommand, IRequest<OperationStatus>
{
}

public class CreateClaimCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateClaimCommand, OperationStatus>
{

    public CreateClaimCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator)
    {

    }

    public async Task<OperationStatus> Handle(CreateClaimCommand command, CancellationToken cancellationToken)
    {
        var existingStoreClaimNumber = await _context.Claims
                                                .Where(c => c.StoreId == command.StoreId
                                                    && c.ClaimNumber == command.ClaimNumber
                                                    && c.ClaimStatusId != (int)ClaimStatusId.Rejected)
                                                .FirstOrDefaultAsync();

        if (existingStoreClaimNumber != null)
        {
            throw new ClaimException("This Store already has a claim with this Claim Number. \n\n It can't be added again.");
        }

        var claim = _mapper.Map<CreateClaimCommand, Claim>(command);

        var dcs = await _mediator.Send(new DistributionCentersByStoreQuery { StoreId = command.StoreId }, cancellationToken);
        if (dcs.Count == 0)
        {
            throw new ClaimException("There is no Distribution Center for the Store");
        }
        //TODO Pass DistributionCenterId from front-end  
        var dc = dcs.Where(e => e.IsPrimary == true).FirstOrDefault();
        if (dc != null)
        {
            claim.DistributionCenterId = dc.Id;
        }
        else
        {
            claim.DistributionCenterId = dcs.OrderBy(e => e.Id).Last().Id;
        }
        //claim.DistributionCenterId = dcs.First().Id;

        var claimPeriod = await _context.ClaimPeriods.SingleOrDefaultAsync(e => DateTime.Now.Date >= e.StartDate.Date &&
                                                                                DateTime.Now.Date <= e.EndDate.Date, cancellationToken);
        if (claimPeriod == null)
        {
            throw new ClaimException("There is no Claim Period for today's date");
        }

        claim.ClaimPeriodId = claimPeriod.ClaimPeriodId;
        claim.ClaimStatusId = (int)ClaimStatusId.Unsubmitted;
        claim.ClaimSupplierStatusId = (int)ClaimSupplierStatusId.Unapproved;

        _context.Claims.Add(claim);
        var opStatus = await _context.SaveChangesAsync(cancellationToken);

        opStatus.OperationId = claim.ClaimId;
        await _mediator.Send(new CreateClaimHistoryCommand(claim), cancellationToken);
        return opStatus;

    }
}
