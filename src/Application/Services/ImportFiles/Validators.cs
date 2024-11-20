using FluentValidation;

namespace Engage.Application.Services.ImportFiles
{
    public class ImportFileValidator<T> : AbstractValidator<T> where T : ImportFileCommand
    {
        public ImportFileValidator()
        {

        }
    }

    public class CreateImportFileValidator : ImportFileValidator<CreateImportFileCommand>
    {
        public CreateImportFileValidator()
        {
            RuleFor(e => e.FileName).NotEmpty().MaximumLength(1000);
        }
    }

    public class UpdateImportFileValidator : ImportFileValidator<UpdateImportFileCommand>
    {
        public UpdateImportFileValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        }
    }

}
