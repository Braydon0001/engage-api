namespace Engage.Application.Services.Claims.Rules.Models;

public class ClaimRuleResult
{
    public bool IsSuccess { get; private set; }
    public string FailureText { get; private set; }
    public ClaimRuleResult(bool passed, string failureText = "")
    {
        IsSuccess = passed;
        FailureText = failureText;
    }
}

public class CanUpdateClaimRuleResult : ClaimRuleResult
{
    public bool IsApprover { get; private set; }
    public bool IsCreator { get; private set; }

    public CanUpdateClaimRuleResult(bool passed, bool isApprover, bool isCreator, string failureText = "") : base(passed, failureText)
    {
        IsApprover = isApprover;
        IsCreator = isCreator;
    }
}
