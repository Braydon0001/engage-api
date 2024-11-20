namespace Engage.Application.Services.GlAdjustments.Commands;

class GLAdjustmentValidator<T> : AbstractValidator<T> where T : GLAdjustmentCommand
{
    public GLAdjustmentValidator()
    {
        RuleFor(x => x.GLDescription).MaximumLength(100).NotEmpty();
        RuleFor(x => x.DebitValue).LessThanOrEqualTo(0).NotEmpty();
        RuleFor(x => x.CreditValue).GreaterThanOrEqualTo(0).NotEmpty();
        RuleFor(x => x.DocumentNo).MaximumLength(20).NotEmpty();
        RuleFor(x => x.Description).MaximumLength(100).NotEmpty();
        RuleFor(x => x.Invoice).MaximumLength(20).NotEmpty();
        RuleFor(x => x.Account).MaximumLength(20).NotEmpty();
        RuleFor(x => x.SupplierId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.GLAdjustmentTypeId).GreaterThan(0).NotEmpty();
    }

    public class CreateGLAdjustmentValidator : GLAdjustmentValidator<CreateGLAdjustmentCommand>
    {
        public CreateGLAdjustmentValidator()
        {

        }
    }

    public class UpdateGLAdjustmentValidator : GLAdjustmentValidator<UpdateGLAdjustmentCommand>
    {
        public UpdateGLAdjustmentValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        }
    }
}
