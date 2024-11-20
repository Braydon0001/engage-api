// auto-generated
namespace Engage.Application.Services.EmployeePensions.Commands;

public class EmployeePensionFileDeleteCommand : FileDeleteCommand, IRequest<bool>
{
}

public class EmployeePensionFileDeleteHandler : FileDeleteHandler, IRequestHandler<EmployeePensionFileDeleteCommand, bool>
{
    public EmployeePensionFileDeleteHandler(IAppDbContext context, IFileService file) : base(context, file)
    {
    }

    public async Task<bool> Handle(EmployeePensionFileDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeePensions.SingleOrDefaultAsync(e => e.EmployeePensionId == command.Id, cancellationToken);
        if (entity == null || entity.Files == null || !entity.Files.FileExists(command))
        {
            return false;
        }

        await _file.DeleteAsync(command, nameof(EmployeePension), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);
        
        await _context.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}

public class EmployeePensionFileDeleteValidator : FileDeleteValidator<EmployeePensionFileDeleteCommand>
{
    public EmployeePensionFileDeleteValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
    }
}