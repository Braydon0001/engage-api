namespace Engage.Application.Services.ClaimStatusUsers.Commands
{
    public class ClaimStatusUserValidator<T> : AbstractValidator<T> where T : ClaimStatusUserCommand
    {
        public ClaimStatusUserValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0).NotEmpty();
            RuleFor(x => x.ClaimStatus).GreaterThan(0).NotEmpty();            
        }
    }

    public class CreateClaimStatusUserValidator : ClaimStatusUserValidator<CreateClaimStatusUserCommand>
    {
        public CreateClaimStatusUserValidator()
        {
        }
    }

    
}
