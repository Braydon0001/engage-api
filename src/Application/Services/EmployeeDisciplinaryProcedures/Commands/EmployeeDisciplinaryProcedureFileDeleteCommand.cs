namespace Engage.Application.Services.EmployeeDisciplinaryProcedures.Commands;

public class EmployeeDisciplinaryProcedureFileDeleteCommand : FileDeleteCommand, IRequest<bool>
{
}

public record EmployeeDisciplinaryProcedureFileDeleteHandler(IAppDbContext Context, IFileService File) : IRequestHandler<EmployeeDisciplinaryProcedureFileDeleteCommand, bool>
{
    public async Task<bool> Handle(EmployeeDisciplinaryProcedureFileDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.EmployeeDisciplinaryProcedures.SingleOrDefaultAsync(e => e.EmployeeDisciplinaryProcedureId == command.Id, cancellationToken);
        if (entity == null || entity.Files == null || !entity.Files.FileExists(command))
        {
            return false;
        }

        await File.DeleteAsync(command, nameof(EmployeeDisciplinaryProcedure), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);

        await Context.SaveChangesAsync(cancellationToken);

        return true;
    }
}

public class EmployeeDisciplinaryProcedureFileDeleteValidator : FileDeleteValidator<EmployeeDisciplinaryProcedureFileDeleteCommand>
{
    public EmployeeDisciplinaryProcedureFileDeleteValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
    }
}