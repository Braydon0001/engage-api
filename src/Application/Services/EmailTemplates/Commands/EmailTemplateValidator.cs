namespace Engage.Application.Services.EmailTemplates.Commands;

class EmailTemplateValidator<T> : AbstractValidator<T> where T : EmailTemplateCommand
{
    public EmailTemplateValidator()
    {
        RuleFor(x => x.EmailTemplateTypeId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.EmailTypeId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Name).MaximumLength(100).NotEmpty();
        RuleFor(x => x.ExternalTemplateId).MaximumLength(100).NotEmpty();
        RuleFor(x => x.FromEmailName).MaximumLength(100).NotEmpty();
        RuleFor(x => x.FromEmailAddress).MaximumLength(100).NotEmpty();
    }

    public class CreateEmailTemplateValidator : EmailTemplateValidator<CreateEmailTemplateCommand>
    {
        public CreateEmailTemplateValidator()
        {

        }
    }

    public class UpdateEmailTemplateValidator : EmailTemplateValidator<UpdateEmailTemplateCommand>
    {
        public UpdateEmailTemplateValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        }
    }
}
