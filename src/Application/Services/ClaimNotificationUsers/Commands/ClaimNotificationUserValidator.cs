namespace Engage.Application.Services.ClaimNotificationUsers.Commands;

public class ClaimNotificationUserValidator<T> : AbstractValidator<T> where T : ClaimNotificationUserCommand
{
    public ClaimNotificationUserValidator()
    {
        RuleFor(x => x.ClaimStatusId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.UserId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.EngageRegionId).GreaterThan(0).NotEmpty();
    }
}

public class CreateClaimNotificationUserValidator : ClaimNotificationUserValidator<CreateClaimNotificationUserCommand>
{
    public CreateClaimNotificationUserValidator()
    {
    }
}
