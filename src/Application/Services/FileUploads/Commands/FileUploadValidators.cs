namespace Engage.Application.Services.FileUploads.Commands;

public class UpdateFileUploadImportDateValidator : AbstractValidator<UpdateFileUploadImportDateCommand>
{
    public UpdateFileUploadImportDateValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
