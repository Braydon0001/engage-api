using Engage.Application.Services.ClaimFloats.Commands;
using Engage.Application.Services.ClaimHistories.Commands;
using Engage.Application.Services.Claims.Rules;
using Engage.Application.Services.Claims.Rules.Models;

namespace Engage.Application.Services.Claims.Commands;

public class UpdateClaimStatusCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
    public int ClaimStatusId { get; set; }
    public int? ClaimRejectedReasonId { get; set; }
    public string RejectedReason { get; set; }
    public ClaimHistoryHeader ClaimHistoryHeader { get; set; }
    public bool SaveChanges { get; set; } = true;
}

public class UpdateClaimStatusCommandHandler : IRequestHandler<UpdateClaimStatusCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;
    private readonly IUserService _user;

    public UpdateClaimStatusCommandHandler(IAppDbContext context, IMediator mediator, IUserService user)
    {
        _context = context;
        _mediator = mediator;
        _user = user;
    }

    public async Task<OperationStatus> Handle(UpdateClaimStatusCommand command, CancellationToken cancellationToken)
    {
        var claim = await _context.Claims
                                    .Include(x => x.Store)
                                    .SingleAsync(x => x.ClaimId == command.Id, cancellationToken);

        var claimRulecontext = new ClaimRuleContext(claim, _context, _user);

        var canUpdate = await ClaimRuleEngine.CanUpdateStatus(command.ClaimStatusId, claimRulecontext, cancellationToken);
        if (!canUpdate.IsSuccess)
        {
            throw new ClaimException(canUpdate.FailureText);
        }

        if (command.ClaimStatusId == (int)ClaimStatusId.Unapproved)
        {
            var mustApprove = await MustApproveRule.Evaluate(claimRulecontext, cancellationToken);
            if (mustApprove)
            {
                claim.ClaimStatusId = (int)ClaimStatusId.Unapproved;
            }
            else
            {
                claim.ClaimStatusId = (int)ClaimStatusId.Approved;
                claim.ApprovedDate = DateTime.Now;
                if (!string.IsNullOrWhiteSpace(_user.UserName))
                {
                    claim.ApprovedBy = _user.UserName;
                }

                await _mediator.Send(new UpdateClaimFloatClaimCommand(claim, claim.Store.EngageRegionId, (int)ClaimStatusId.Approved, false), cancellationToken);
            }
        }
        else
        {
            claim.ClaimStatusId = command.ClaimStatusId;
        }

        UpdateHistoryProperties(command, claim);

        if (command.ClaimStatusId == (int)ClaimStatusId.Approved || command.ClaimStatusId == (int)ClaimStatusId.Rejected)
        {
            await _mediator.Send(new UpdateClaimFloatClaimCommand(claim, claim.Store.EngageRegionId, command.ClaimStatusId, false), cancellationToken);
        }

        await _mediator.Send(new CreateClaimHistoryCommand(claim, command.ClaimHistoryHeader, false), cancellationToken);

        if (command.SaveChanges)
        {
            var opStatus = await _context.SaveChangesAsync(cancellationToken);
            opStatus.OperationId = command.Id;
            return opStatus;
        }

        return new OperationStatus(status: true);
    }

    private void UpdateHistoryProperties(UpdateClaimStatusCommand command, Claim claim)
    {
        switch (command.ClaimStatusId)
        {
            case (int)ClaimStatusId.Unapproved:
                claim.UnapprovedDate = DateTime.Now;
                if (!string.IsNullOrWhiteSpace(_user.UserName))
                {
                    claim.UnapprovedBy = _user.UserName;
                }
                break;
            case (int)ClaimStatusId.Approved:
                claim.ApprovedDate = DateTime.Now;
                if (!string.IsNullOrWhiteSpace(_user.UserName))
                {
                    claim.ApprovedBy = _user.UserName;
                }
                break;
            case (int)ClaimStatusId.Rejected:
                claim.RejectedDate = DateTime.Now;
                if (!string.IsNullOrWhiteSpace(_user.UserName))
                {
                    claim.RejectedBy = _user.UserName;
                }
                if (command.ClaimRejectedReasonId.HasValue)
                {
                    claim.ClaimRejectedReasonId = command.ClaimRejectedReasonId.Value;
                }
                if (!string.IsNullOrWhiteSpace(command.RejectedReason))
                {
                    claim.RejectedReason = command.RejectedReason;
                }
                break;
            case (int)ClaimStatusId.Paid:
                claim.PaidDate = DateTime.Now;
                if (!string.IsNullOrWhiteSpace(_user.UserName))
                {
                    claim.PaidBy = _user.UserName;
                }
                break;
        }
    }
}
