namespace Engage.Application.Services.Organizations.Commands;

public class OrganizationFileDeleteCommand : FileDeleteCommand, IRequest<bool>
{
}

public class OrganizationFileDeleteHandler : FileDeleteHandler, IRequestHandler<OrganizationFileDeleteCommand, bool>
{
    public OrganizationFileDeleteHandler(IAppDbContext context, IFileService file) : base(context, file)
    {
    }

    public async Task<bool> Handle(OrganizationFileDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Organizations.SingleOrDefaultAsync(e => e.OrganizationId == command.Id, cancellationToken);
        if (entity == null || entity.Files == null || !entity.Files.FileExists(command))
        {
            return false;
        }

        await _file.DeleteAsync(command, nameof(Organization), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}

public class OrganizationFileDeleteValidator : FileDeleteValidator<OrganizationFileDeleteCommand>
{
    public OrganizationFileDeleteValidator()
    {
    }
}