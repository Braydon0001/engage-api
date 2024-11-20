using FluentValidation;

namespace Engage.Application.Services.ImportSurveyStores
{
    public class ImportSurveyStoresValidator : AbstractValidator<ImportSurveyStoresCommand>
    {
        public ImportSurveyStoresValidator()
        {
            RuleFor(e => e.SurveyId).GreaterThan(0).NotEmpty();
            RuleFor(e => e.StoreFormatIds).NotEmpty();
            RuleForEach(e => e.StoreFormatIds).GreaterThan(0);
            RuleFor(e => e.File).NotNull();
        }
    }

    public class ProcessSurveyStoresImportValidator : AbstractValidator<ProcessSurveyStoresImportCommand>
    {
        public ProcessSurveyStoresImportValidator()
        {
            RuleFor(e => e.ImportFileId).GreaterThan(0).NotEmpty();
            RuleFor(e => e.SurveyId).GreaterThan(0).NotEmpty();
        }
    }
}
