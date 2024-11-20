namespace Engage.Application.Services.EmployeeSkills.Commands;

public class EmployeeSkillUploadFileCommand : FileUploadCommand, IRequest<OperationStatus>
{
}

public class EmployeeSkillUploadFileHandler : FileUploadHandler, IRequestHandler<EmployeeSkillUploadFileCommand, OperationStatus>
{
    public EmployeeSkillUploadFileHandler(IAppDbContext context, IFileService fileStorage) : base(context, fileStorage)
    {
    }

    public async Task<OperationStatus> Handle(EmployeeSkillUploadFileCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeSkills.SingleOrDefaultAsync(e => e.EmployeeSkillId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(EmployeeCoolerBox),
            EntityFiles = entity.Files,
            MaxFiles = 1
        };

        var file = await _file.UploadAsync(command, options, cancellationToken);

        entity.Files = entity.Files.AddFile(file);

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = command.Id;
        operationStatus.ReturnObject = file;
        return operationStatus;
    }
}

public class EmployeeSkillUploadFileValidator : FileUploadValidator<EmployeeSkillUploadFileCommand>
{
    public EmployeeSkillUploadFileValidator()
    {
    }
}