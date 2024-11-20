namespace Engage.Application.Services.EmployeeDisciplinaryProcedures.Commands;

public class EmployeeDisciplinaryProcedureFileInsertCommand : FileUploadCommand, IRequest<JsonFile>
{
}

public record EmployeeDisciplinaryProcedureFileInsertHandler(IAppDbContext Context, IFileService File) : IRequestHandler<EmployeeDisciplinaryProcedureFileInsertCommand, JsonFile>
{
    public async Task<JsonFile> Handle(EmployeeDisciplinaryProcedureFileInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.EmployeeDisciplinaryProcedures.SingleOrDefaultAsync(e => e.EmployeeDisciplinaryProcedureId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(EmployeeDisciplinaryProcedure),
            EntityFiles = entity.Files,
            MaxFiles = 1
        };
        var file = await File.UploadAsync(command, options, cancellationToken);

        entity.Files = entity.Files.AddFile(file);

        await Context.SaveChangesAsync(cancellationToken);

        return file;
    }
}

public class EmployeeDisciplinaryProcedureFileInsertValidator : FileUploadValidator<EmployeeDisciplinaryProcedureFileInsertCommand>
{
    public EmployeeDisciplinaryProcedureFileInsertValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.File).NotNull();
    }
}