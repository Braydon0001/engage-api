namespace Engage.Application.Services.EmployeeFiles.Commands;

public class EmployeeFileFileDeleteCommand : FileDeleteCommand, IRequest<bool>
{
}

public class EmployeeFileFileDeleteHandler : FileDeleteHandler, IRequestHandler<EmployeeFileFileDeleteCommand, bool>
{
    public EmployeeFileFileDeleteHandler(IAppDbContext context, IFileService file) : base(context, file)
    {
    }

    public async Task<bool> Handle(EmployeeFileFileDeleteCommand command, CancellationToken cancellationToken)
    {
        var fileType = await _context.EmployeeFileTypes.SingleOrDefaultAsync(e => e.Name.ToLower() == command.FileType.ToLower(), cancellationToken);
        var entities = await _context.EmployeeFiles.Where(e => e.EmployeeId == command.Id && e.EmployeeFileTypeId == fileType.EmployeeFileTypeId).ToListAsync(cancellationToken);
        if (entities.IsNullOrEmpty())
        {
            return false;
        }
        var entity = entities.FirstOrDefault(e => e.Files.Where(e => e.Name == command.FileName).ToList().Count > 0);
        if (entity == null || entity.Files == null || !entity.Files.FileExists(command))
        {
            return false;
        }

        await _file.DeleteAsync(command, nameof(EmployeeFile), cancellationToken);

        _context.EmployeeFiles.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}

public class EmployeeFileFileDeleteValidator : FileDeleteValidator<EmployeeFileFileDeleteCommand>
{
    public EmployeeFileFileDeleteValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
    }
}