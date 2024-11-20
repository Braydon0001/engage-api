namespace Engage.Application.Services.GlAccountTypes.Commands;

class GLAccountTypeValidator<T> : AbstractValidator<T> where T : GLAccountTypeCommand
{
    public GLAccountTypeValidator()
    {
        RuleFor(x => x.Name).MaximumLength(20).NotEmpty();
        RuleFor(x => x.Description).MaximumLength(100).NotEmpty();
    }

    public class CreateGLAccountTypeValidator : GLAccountTypeValidator<CreateGLAccountTypeCommand>
    {
        public CreateGLAccountTypeValidator()
        {

        }
    }

    public class UpdateGLAccountTypeValidator : GLAccountTypeValidator<UpdateGLAccountTypeCommand>
    {
        public UpdateGLAccountTypeValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        }
    }
}
