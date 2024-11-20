using Engage.Application.Services.Claims.Rules.Models;

namespace Engage.Application.Services.Claims.Commands;

// Commands

public class UpdateClaimCommand : ClaimCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateClaimDateCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
    public DateTime ClaimDate { get; set; }
}

public class UpdateClaimNumberCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
    public string ClaimNumber { get; set; }
}

public class UpdateClaimReferenceCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
    public string ClaimReference { get; set; }
}

public class UpdateClaimSupplierIdCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
    public int SupplierId { get; set; }
}

public class UpdateClaimStoreIdCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
    public int StoreId { get; set; }
}

public class UpdateClaimIsPayStoreCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
    public bool IsPayStore { get; set; }
}

public class UpdateClaimIsClaimFromSupplierCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
    public bool IsClaimFromSupplier { get; set; }
}

public class UpdateClaimAccountManagerIdCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
    public int? ClaimAccountManagerId { get; set; }
}

public class UpdateClaimFloatIdCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
    public int? ClaimFloatId { get; set; }
}

public class UpdateClaimDivisionIdCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
    public int? EmployeeDivisionId { get; set; }
}

public class UpdateClaimManagerIdCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
    public int? ClaimManagerId { get; set; }
}

// Command Handlers
public class UpdateClaimCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateClaimCommand, OperationStatus>
{
    private readonly IUserService _user;

    public UpdateClaimCommandHandler(IAppDbContext context, IMapper mapper, IUserService user) : base(context, mapper)
    {
        _user = user;
    }

    public async Task<OperationStatus> Handle(UpdateClaimCommand command, CancellationToken cancellationToken)
    {
        return await ClaimUpdater.Update(command.Id,
                                         _context,
                                         _user,
                                         (claim) => _mapper.Map(command, claim),
                                         cancellationToken);
    }
}

public class UpdateClaimDateCommandHandler : IRequestHandler<UpdateClaimDateCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IUserService _user;

    public UpdateClaimDateCommandHandler(IAppDbContext context, IUserService user)
    {
        _context = context;
        _user = user;
    }

    public async Task<OperationStatus> Handle(UpdateClaimDateCommand command, CancellationToken cancellationToken)
    {
        return await ClaimUpdater.Update(command.Id,
                                            _context,
                                            _user,
                                            (claim) => claim.ClaimDate = command.ClaimDate,
                                            cancellationToken);
    }
}

public class UpdateClaimNumberCommandHandler : IRequestHandler<UpdateClaimNumberCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IUserService _user;

    public UpdateClaimNumberCommandHandler(IAppDbContext context, IUserService user)
    {
        _context = context;
        _user = user;
    }

    public async Task<OperationStatus> Handle(UpdateClaimNumberCommand command, CancellationToken cancellationToken)
    {
        var originalClaim = await _context.Claims.Where(c => c.ClaimId == command.Id).FirstOrDefaultAsync();

        var existingCheck = await _context.Claims
                                    .Where(c => c.StoreId == originalClaim.StoreId
                                           && c.ClaimNumber == command.ClaimNumber
                                           && c.ClaimId != command.Id
                                           && c.ClaimStatusId != (int)ClaimStatusId.Rejected)
                                    .FirstOrDefaultAsync();

        if (existingCheck != null)
        {
            throw new ClaimException("This Store already has a claim with this Claim Number. \n\n It can't be added again.");
        }

        return await ClaimUpdater.Update(command.Id,
                                              _context,
                                              _user,
                                              (claim) => claim.ClaimNumber = command.ClaimNumber,
                                              cancellationToken);
    }
}

public class UpdateClaimReferenceCommandHandler : IRequestHandler<UpdateClaimReferenceCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IUserService _user;

    public UpdateClaimReferenceCommandHandler(IAppDbContext context, IUserService user)
    {
        _context = context;
        _user = user;
    }

    public async Task<OperationStatus> Handle(UpdateClaimReferenceCommand command, CancellationToken cancellationToken)
    {
        return await ClaimUpdater.Update(command.Id,
                                         _context,
                                         _user,
                                         (claim) => claim.ClaimReference = command.ClaimReference,
                                         cancellationToken);
    }
}

public class UpdateClaimSupplierIdCommandHandler : IRequestHandler<UpdateClaimSupplierIdCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IUserService _user;
    private readonly IMediator _mediator;

    public UpdateClaimSupplierIdCommandHandler(IAppDbContext context, IUserService user, IMediator mediator)
    {
        _context = context;
        _user = user;
        _mediator = mediator;
    }

    public async Task<OperationStatus> Handle(UpdateClaimSupplierIdCommand command, CancellationToken cancellationToken)
    {
        var updateClaimAccountManagerIdCommand = new UpdateClaimAccountManagerIdCommand
        {
            ClaimAccountManagerId = null,
            Id = command.Id
        };

        var result = await _mediator.Send(updateClaimAccountManagerIdCommand);

        var updateClaimFloatIdCommand = new UpdateClaimFloatIdCommand
        {
            ClaimFloatId = null,
            Id = command.Id
        };

        var claimFloatResult = await _mediator.Send(updateClaimFloatIdCommand);

        return await ClaimUpdater.Update(command.Id,
                                            _context,
                                            _user,
                                            (claim) => claim.SupplierId = command.SupplierId,
                                            cancellationToken);
    }
}

public class UpdateClaimStoreIdCommandHandler : IRequestHandler<UpdateClaimStoreIdCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IUserService _user;
    private readonly IMediator _mediator;

    public UpdateClaimStoreIdCommandHandler(IAppDbContext context, IUserService user, IMediator mediator)
    {
        _context = context;
        _user = user;
        _mediator = mediator;
    }

    public async Task<OperationStatus> Handle(UpdateClaimStoreIdCommand command, CancellationToken cancellationToken)
    {
        var originalClaim = await _context.Claims.Where(c => c.ClaimId == command.Id).FirstOrDefaultAsync();

        var existingCheck = await _context.Claims
                                    .Where(c => c.StoreId == command.StoreId
                                           && c.ClaimNumber == originalClaim.ClaimNumber
                                           && c.ClaimId != command.Id
                                           && c.ClaimStatusId != (int)ClaimStatusId.Rejected)
                                    .FirstOrDefaultAsync();

        if (existingCheck != null)
        {
            throw new ClaimException("This Store already has a claim with this Claim Number. \n\n It can't be added again.");
        }

        var updateClaimManagerIdCommand = new UpdateClaimManagerIdCommand
        {
            ClaimManagerId = null,
            Id = command.Id
        };

        var result = await _mediator.Send(updateClaimManagerIdCommand);

        var updateClaimFloatIdCommand = new UpdateClaimFloatIdCommand
        {
            ClaimFloatId = null,
            Id = command.Id
        };

        var claimFloatResult = await _mediator.Send(updateClaimFloatIdCommand);

        return await ClaimUpdater.Update(command.Id,
                                            _context,
                                            _user,
                                            (claim) => claim.StoreId = command.StoreId,
                                            cancellationToken);
    }
}

public class UpdateClaimIsPayStoreCommandHandler : IRequestHandler<UpdateClaimIsPayStoreCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IUserService _user;

    public UpdateClaimIsPayStoreCommandHandler(IAppDbContext context, IUserService user)
    {
        _context = context;
        _user = user;
    }

    public async Task<OperationStatus> Handle(UpdateClaimIsPayStoreCommand command, CancellationToken cancellationToken)
    {
        return await ClaimUpdater.Update(command.Id,
                                            _context,
                                            _user,
                                            (claim) => claim.IsPayStore = command.IsPayStore,
                                            cancellationToken);
    }
}

public class UpdateClaimIsClaimFromSupplierCommandHandler : IRequestHandler<UpdateClaimIsClaimFromSupplierCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IUserService _user;

    public UpdateClaimIsClaimFromSupplierCommandHandler(IAppDbContext context, IUserService user)
    {
        _context = context;
        _user = user;
    }

    public async Task<OperationStatus> Handle(UpdateClaimIsClaimFromSupplierCommand command, CancellationToken cancellationToken)
    {
        return await ClaimUpdater.Update(command.Id,
                                            _context,
                                            _user,
                                            (claim) => claim.IsClaimFromSupplier = command.IsClaimFromSupplier,
                                            cancellationToken);
    }
}

public class UpdateClaimAccountManagerIdCommandHandler : IRequestHandler<UpdateClaimAccountManagerIdCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IUserService _user;

    public UpdateClaimAccountManagerIdCommandHandler(IAppDbContext context, IUserService user)
    {
        _context = context;
        _user = user;
    }

    public async Task<OperationStatus> Handle(UpdateClaimAccountManagerIdCommand command, CancellationToken cancellationToken)
    {
        return await ClaimUpdater.Update(command.Id,
                                            _context,
                                            _user,
                                            (claim) => claim.ClaimAccountManagerId = command.ClaimAccountManagerId,
                                            cancellationToken);
    }
}

public class UpdateClaimFloatIdCommandHandler : IRequestHandler<UpdateClaimFloatIdCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IUserService _user;

    public UpdateClaimFloatIdCommandHandler(IAppDbContext context, IUserService user)
    {
        _context = context;
        _user = user;
    }

    public async Task<OperationStatus> Handle(UpdateClaimFloatIdCommand command, CancellationToken cancellationToken)
    {
        return await ClaimUpdater.Update(command.Id,
                                            _context,
                                            _user,
                                            (claim) => claim.ClaimFloatId = command.ClaimFloatId,
                                            cancellationToken);
    }
}

public class UpdateClaimDivisionIdCommandHandler : IRequestHandler<UpdateClaimDivisionIdCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IUserService _user;

    public UpdateClaimDivisionIdCommandHandler(IAppDbContext context, IUserService user)
    {
        _context = context;
        _user = user;
    }

    public async Task<OperationStatus> Handle(UpdateClaimDivisionIdCommand command, CancellationToken cancellationToken)
    {
        return await ClaimUpdater.Update(command.Id,
                                            _context,
                                            _user,
                                            (claim) => claim.EmployeeDivisionId = command.EmployeeDivisionId,
                                            cancellationToken);
    }
}

public class UpdateClaimManagerIdCommandHandler : IRequestHandler<UpdateClaimManagerIdCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IUserService _user;

    public UpdateClaimManagerIdCommandHandler(IAppDbContext context, IUserService user)
    {
        _context = context;
        _user = user;
    }

    public async Task<OperationStatus> Handle(UpdateClaimManagerIdCommand command, CancellationToken cancellationToken)
    {
        return await ClaimUpdater.Update(command.Id,
                                            _context,
                                            _user,
                                            (claim) => claim.ClaimManagerId = command.ClaimManagerId,
                                            cancellationToken);
    }
}

public static class ClaimUpdater
{
    public static async Task<OperationStatus> Update(int claimId, IAppDbContext context, IUserService user, Action<Claim> updateClaimAction, CancellationToken cancellationToken)
    {
        var claim = await context.Claims.SingleAsync(x => x.ClaimId == claimId, cancellationToken);
        updateClaimAction(claim);

        var result = await ClaimRuleEngine.CanUpdate(new ClaimRuleContext(claim, context, user), cancellationToken);
        if (!result.IsSuccess)
        {
            throw new ClaimException(result.FailureText);
        }

        var opStatus = await context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = claimId;
        return opStatus;

    }
}
