using Engage.Application.Services.Claims.Rules;
using Engage.Application.Services.Claims.Rules.Models;

namespace Engage.Application.Services.Claims;

public static class ClaimRuleEngine
{
    public static async Task<CanUpdateClaimRuleResult> CanUpdate(ClaimRuleContext context, CancellationToken cancellationToken)
    {
        var text = "The claim can't be updated.\n";
        var failedText = string.Empty;
        var claimStatusId = context.Claim.ClaimStatusId;

        var isApprover = await IsApproverRule.Evaluate(context, cancellationToken);
        var isCreator = await IsCreatorRule.Evaluate(context, cancellationToken);

        switch (claimStatusId)
        {
            case (int)ClaimStatusId.Unsubmitted:
            case (int)ClaimStatusId.Unapproved:
                if (isApprover)
                {
                    if (!(await InUserRegionRule.Evaluate(context, cancellationToken)))
                    {
                        failedText = $"{text} A claim can only be updated if it is in your region";
                    }
                }
                else if (!isCreator)
                {
                    failedText = $"{text} It can only be updated by: '{context.Claim.CreatedBy ?? ""}' or a claim approver";
                }
                break;
            case (int)ClaimStatusId.Approved:
                if (!(await InApprovalPeriodRule.Evaluate(context, cancellationToken)) && !(await IsEditAfterApprovalPeriodRule.Evaluate(context, cancellationToken)))
                {
                    failedText = $"{text} A claim cannot be updated after it's cut-off point";
                }
                else if (isApprover)
                {
                    if (!(await InUserRegionRule.Evaluate(context, cancellationToken)))
                    {
                        failedText = $"{text} A claim can only be updated if it is in your region";
                    }
                }
                else if (!isApprover && !isCreator)
                {
                    failedText = $"{text} It can only be updated by a claim approver or creator";
                }
                break;
            case (int)ClaimStatusId.Rejected:
            case (int)ClaimStatusId.Paid:
                failedText = $"{text} A {StatusName(claimStatusId)} claim can't be updated.";
                break;
        }

        return string.IsNullOrWhiteSpace(failedText)
        ? await Task.FromResult(new CanUpdateClaimRuleResult(true, isApprover, isCreator))
        : await Task.FromResult(new CanUpdateClaimRuleResult(false, isApprover, isCreator, failedText));
    }

    public static async Task<ClaimRuleResult> CanUpdateStatus(int newClaimStatusId, ClaimRuleContext context, CancellationToken cancellationToken)
    {
        var claimStatusId = context.Claim.ClaimStatusId;
        string Text() => $"Claim {context.Claim.ClaimId} can't be updated to {StatusName(newClaimStatusId)}.\n";
        var failedMessage = string.Empty;

        var isApprover = await IsApproverRule.Evaluate(context, cancellationToken);
        var isCreator = await IsCreatorRule.Evaluate(context, cancellationToken);

        if (await IsSameStatusRule.Evaluate(newClaimStatusId, context, cancellationToken))
        {
            failedMessage = $"{Text()} It is already in a {StatusName(claimStatusId)} status.";
        }
        else
        {
            switch (newClaimStatusId)
            {
                // No claim can be Unsubmitted. (A claim is created in an Unsubmitted status).
                case (int)ClaimStatusId.Unsubmitted:
                    {
                        failedMessage = $"{Text()} A Claim can't be Unsubmitted.";
                    };
                    break;
                case (int)ClaimStatusId.Unapproved:
                    if (claimStatusId != (int)ClaimStatusId.Unsubmitted)
                    {
                        failedMessage = $"{Text()} Only an Unsubmitted Claim can be Unapproved.";
                    }
                    break;
                case (int)ClaimStatusId.Approved:
                    if (claimStatusId != (int)ClaimStatusId.Unapproved)
                    {
                        failedMessage = $"{Text()} Only an Unapproved Claim can be Approved.";
                    }
                    break;
                case (int)ClaimStatusId.Rejected:
                    if (claimStatusId != (int)ClaimStatusId.Unapproved && claimStatusId != (int)ClaimStatusId.Approved)
                    {
                        failedMessage = $"{Text()} Only an Unapproved or Approved Claim can be Rejected.";
                    }

                    if ((claimStatusId == (int)ClaimStatusId.Approved) && (!await InApprovalPeriodRule.Evaluate(context, cancellationToken)) && (!await IsProcessAfterApprovalPeriodRule.Evaluate(context, cancellationToken)))
                    {
                        failedMessage = $"{Text()} An Approved Claim can not be rejected after it's cut-off point.";
                    }

                    if (claimStatusId == (int)ClaimStatusId.Unapproved)
                    {
                        if (!isApprover && !isCreator)
                        {
                            failedMessage = $"{Text()} An Unapproved Claim can only be rejected by a Creator or an Approver.";
                        }
                    }

                    break;
                case (int)ClaimStatusId.Paid:
                    if (claimStatusId != (int)ClaimStatusId.Approved)
                    {
                        failedMessage = $"{Text()} Only an Approved Claim can be Paid.";
                    }
                    break;
            }
        }

        return string.IsNullOrWhiteSpace(failedMessage)
        ? await Task.FromResult(new ClaimRuleResult(true))
        : await Task.FromResult(new ClaimRuleResult(false, failedMessage));
    }

    public static async Task<ClaimRuleResult> CanUpdateSupplierStatus(int newClaimSupplierStatusId, ClaimRuleContext context, CancellationToken cancellationToken)
    {
        var claimSupplierStatusId = context.Claim.ClaimSupplierStatusId;
        string Text() => $"Claim {context.Claim.ClaimId} can't be updated to {SupplierStatusName(newClaimSupplierStatusId)}.\n";
        var failedMessage = string.Empty;

        if (await IsSameSupplierStatusRule.Evaluate(newClaimSupplierStatusId, context, cancellationToken))
        {
            failedMessage = $"{Text()} It is already in a {SupplierStatusName(claimSupplierStatusId)} status.";
        }
        else
        {
            switch (newClaimSupplierStatusId)
            {
                // No claim can be Unapproved. (A claim is created in an Unapproved supplier status).
                case (int)ClaimSupplierStatusId.Unapproved:
                    {
                        failedMessage = $"{Text()} A Claim can't be Unapproved.";
                    };
                    break;

                case (int)ClaimSupplierStatusId.Approved:
                    if (claimSupplierStatusId != (int)ClaimSupplierStatusId.Unapproved && claimSupplierStatusId != (int)ClaimSupplierStatusId.Disputed)
                    {
                        failedMessage = $"{Text()} Only an Unapproved Claim or Pending Claim can be Approved.";
                    }
                    break;
                case (int)ClaimSupplierStatusId.Disputed:
                    if (claimSupplierStatusId != (int)ClaimSupplierStatusId.Unapproved && claimSupplierStatusId != (int)ClaimSupplierStatusId.Approved)
                    {
                        failedMessage = $"{Text()} Only an Unapproved or Approved Claim can be Disputed.";
                    }
                    break;
            }
        }

        return string.IsNullOrWhiteSpace(failedMessage)
        ? await Task.FromResult(new ClaimRuleResult(true))
        : await Task.FromResult(new ClaimRuleResult(false, failedMessage));
    }

    private static string StatusName(int status)
    {
        return Enum.GetName(typeof(ClaimStatusId), status);
    }

    private static string SupplierStatusName(int supplierStatus)
    {
        return Enum.GetName(typeof(ClaimSupplierStatusId), supplierStatus);
    }
}