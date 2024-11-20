namespace Engage.Application.Services.ClaimFloats.Commands;

public class ClaimFloatValidators
{
    class ClaimFloatValidator<T> : AbstractValidator<T> where T : ClaimFloatCommand
    {
        public ClaimFloatValidator()
        {
            RuleFor(x => x.EngageRegionId).GreaterThan(0).NotEmpty();
            RuleFor(x => x.SupplierId).GreaterThan(0).NotEmpty();
            RuleFor(x => x.Title).MaximumLength(300).NotEmpty();
            RuleFor(x => x.Reference).MaximumLength(220);
        }

        public class CreateClaimFloatValidator : ClaimFloatValidator<CreateClaimFloatCommand>
        {
            public CreateClaimFloatValidator()
            {

            }
        }

        public class UpdateClaimFloatValidator : ClaimFloatValidator<UpdateClaimFloatCommand>
        {
            public UpdateClaimFloatValidator()
            {
                RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
            }
        }
    }
}
