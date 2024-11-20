namespace Engage.Application.Services.EmployeeFiles.Commands;

public class EmployeeFileFileUploadCommand : FileUploadCommand, IRequest<JsonFile>
{
    public int EmployeeId { get; set; }
}

public record EmployeeFileFileUploadHandler(IAppDbContext Context, IFileService File, IMediator Mediator) : IRequestHandler<EmployeeFileFileUploadCommand, JsonFile>
{
    public async Task<JsonFile> Handle(EmployeeFileFileUploadCommand command, CancellationToken cancellationToken)
    {
        var employee = await Context.Employees.SingleOrDefaultAsync(e => e.EmployeeId == command.EmployeeId, cancellationToken);
        var employeeFileType = await Context.EmployeeFileTypes
                                            .IgnoreQueryFilters()
                                            .SingleOrDefaultAsync(e => e.Name.ToLower() == command.FileType.ToLower()
                                                && e.Deleted == false && e.Disabled == false, cancellationToken);
        if (employee == null)
        {
            return null;
        }
        if (employeeFileType == null)
        {
            return null;
        }

        var entity = await Mediator.Send(new EmployeeFileInsertCommand
        {
            EmployeeId = command.EmployeeId,
            EmployeeFileTypeId = employeeFileType.EmployeeFileTypeId
        }, cancellationToken);

        var options = new FileUploadOptions
        {
            ContainerName = nameof(EmployeeFile),
            EntityFiles = entity.Files,
            MaxFiles = 1,
            OverwriteType = false
        };

        command.Id = entity.EmployeeFileId;
        var file = await File.UploadAsync(command, options, cancellationToken);

        entity.Files = entity.Files.AddFile(file);

        await Context.SaveChangesAsync(cancellationToken);

        return file;
    }
}

public class EmployeeFileFileUploadValidator : FileUploadValidator<EmployeeFileFileUploadCommand>
{
    public EmployeeFileFileUploadValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.File).NotNull();
    }
}