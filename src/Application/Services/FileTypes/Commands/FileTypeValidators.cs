namespace Engage.Application.Services.FileTypes.Commands;

public class FileTypeValidator<T> : AbstractValidator<T> where T : FileTypeCommand
{
    public FileTypeValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(30);
    }
}

public class CreateFileTypeValidator : FileTypeValidator<FileTypeCreateCommand>
{
    public CreateFileTypeValidator()
    {
    }
}

public class UpdateFileTypeValidator : FileTypeValidator<FileTypeUpdateCommand>
{
    public UpdateFileTypeValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}