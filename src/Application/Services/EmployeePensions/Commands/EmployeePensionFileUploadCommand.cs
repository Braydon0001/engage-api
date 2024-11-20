// auto-generated
namespace Engage.Application.Services.EmployeePensions.Commands;

public class EmployeePensionFileUploadCommand : FileUploadCommand, IRequest<JsonFile>
{
}

public class EmployeePensionFileUploadHandler : FileUploadHandler, IRequestHandler<EmployeePensionFileUploadCommand, JsonFile>
{
    public EmployeePensionFileUploadHandler(IAppDbContext context, IFileService file) : base(context, file)
    {
    }

    public async Task<JsonFile> Handle(EmployeePensionFileUploadCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeePensions.SingleOrDefaultAsync(e => e.EmployeePensionId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(EmployeePension),
            EntityFiles = entity.Files,
            MaxFiles = 5
        };
        var file = await _file.UploadAsync(command, options, cancellationToken);

        entity.Files = entity.Files.AddFile(file);

        await _context.SaveChangesAsync(cancellationToken);

        return file;
    }
}

public class EmployeePensionFileUploadValidator : FileUploadValidator<EmployeePensionFileUploadCommand>
{
    public EmployeePensionFileUploadValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.File).NotNull();
    }
}