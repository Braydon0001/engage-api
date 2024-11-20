namespace Engage.Application.Services.EmployeeSkills.Commands;

public class EmployeeSkillDeleteFileCommand : FileDeleteCommand, IRequest<OperationStatus>
{
}

public class EmployeeSkillDeleteFileHandler : FileDeleteHandler, IRequestHandler<EmployeeSkillDeleteFileCommand, OperationStatus>
{
    public EmployeeSkillDeleteFileHandler(IAppDbContext context, IFileService fileStorage) : base(context, fileStorage)
    {
    }

    public async Task<OperationStatus> Handle(EmployeeSkillDeleteFileCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeSkills.SingleOrDefaultAsync(e => e.EmployeeSkillId == command.Id, cancellationToken);
        if (entity == null || !entity.Files.FileExists(command))
        {
            return null;
        }

        await _file.DeleteAsync(command, nameof(EmployeeSkill), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = command.Id;
        return operationStatus;
    }
}

public class EmployeeSkillDeleteFileValidator : FileDeleteValidator<EmployeeSkillDeleteFileCommand>
{
    public EmployeeSkillDeleteFileValidator()
    {
    }
}