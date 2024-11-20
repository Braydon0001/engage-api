using Engage.Application.Services.ClaimClassifications.Queries;
using Engage.Application.Services.Claims.Models;
using Engage.Application.Services.Claims.Rules.Models;
using Engage.Application.Services.ClaimTypes.Queries;
using Engage.Application.Services.VatPeriods.Queries;

namespace Engage.Application.Services.Claims.Queries;

public class ClaimVmQuery : GetByIdQuery, IRequest<ClaimVm>
{
}

public class ClaimVMQueryHandler : BaseQueryHandler, IRequestHandler<ClaimVmQuery, ClaimVm>
{
    private readonly IMediator _mediator;
    private readonly IUserService _user;

    public ClaimVMQueryHandler(IAppDbContext context, IMapper mapper, IMediator mediator, IUserService user) : base(context, mapper)
    {
        _mediator = mediator;
        _user = user;
    }

    public async Task<ClaimVm> Handle(ClaimVmQuery request, CancellationToken cancellationToken)
    {
        var claim = await _context.Claims.IgnoreQueryFilters()
                                          .Include(e => e.ClientType)
                                          .Include(e => e.ClaimStatus)
                                          .Include(e => e.ClaimSupplierStatus)
                                          .Include(e => e.Vat)
                                          .Include(e => e.DistributionCenter)
                                          .Include(e => e.ClaimAccountManager)
                                          .Include(e => e.ClaimManager)
                                          .Include(e => e.ClaimBlobs)
                                          .Include(e => e.ClaimFloat)
                                          .Include(e => e.EmployeeDivision)
                                          .SingleAsync(x => x.ClaimId == request.Id, cancellationToken);


        var claimRuleContext = new ClaimRuleContext(claim, _context, _user);

        var mappedClaim = _mapper.Map<Claim, ClaimVm>(claim);

        mappedClaim.ClaimClassificationId = await _mediator.Send(new ClaimClassificationVmQuery { Id = claim.ClaimClassificationId }, cancellationToken);
        mappedClaim.ClaimTypeId = await _mediator.Send(new ClaimTypeVmQuery { Id = claim.ClaimTypeId }, cancellationToken);

        var supplier = await _context.Suppliers.SingleAsync(e => e.SupplierId == claim.SupplierId, cancellationToken);
        mappedClaim.SupplierId = _mapper.Map<Supplier, ClaimSupplierOptionDto>(supplier);

        var store = await _context.Stores.Include(e => e.EngageRegion)
                                         .SingleAsync(e => e.StoreId == claim.StoreId, cancellationToken);
        mappedClaim.StoreId = _mapper.Map<Store, ClaimStoreOptionDto>(store);

        mappedClaim.CanUpdate = await ClaimRuleEngine.CanUpdate(claimRuleContext, cancellationToken);

        var vatPercent = await _mediator.Send(new VatPeriodPercentQuery(claim.VatId), cancellationToken);
        if (vatPercent == 0)
        {
            mappedClaim.UpdateMessage = "This claim has no Vat because the claim type is zero-rated.";
        }

        return mappedClaim;
    }
}
