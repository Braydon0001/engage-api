namespace Engage.Application.Services.FileContainers.Commands;

public class FileContainerValidator<T> : AbstractValidator<T> where T : FileContainerCommand
{
    public FileContainerValidator()
    {
        RuleFor(x => x.Name).MaximumLength(120).NotEmpty();
        RuleFor(x => x.ContainerName).MaximumLength(1024).NotEmpty();
        RuleFor(x => x.FileNameStrategy).MaximumLength(30).NotEmpty();
    }
}

public class CreateFileContainerValidator : FileContainerValidator<CreateFileContainerCommand>
{
    public CreateFileContainerValidator()
    {
    }
}

public class UpdateFileContainerValidator : FileContainerValidator<UpdateFileContainerCommand>
{
    public UpdateFileContainerValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
