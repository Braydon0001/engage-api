namespace Engage.Application.Services.GLAccounts.Commands;

class GLAccountValidator<T> : AbstractValidator<T> where T : GLAccountCommand
{
    public GLAccountValidator()
    {
        RuleFor(x => x.GLAccountTypeId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.EngageRegionId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Code).MaximumLength(20).NotEmpty();
        RuleFor(x => x.Name).MaximumLength(100).NotEmpty();
    }

    public class CreateGLAccountValidator : GLAccountValidator<CreateGLAccountCommand>
    {
        public CreateGLAccountValidator()
        {

        }
    }

    public class UpdateGLAccountValidator : GLAccountValidator<UpdateGLAccountCommand>
    {
        public UpdateGLAccountValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        }
    }
}
