namespace Engage.Application.Services.Organizations.Commands;

public class OrganizationFileUploadCommand : FileUploadCommand, IRequest<JsonFile>
{
}

public class OrganizationFileUploadHandler : FileUploadHandler, IRequestHandler<OrganizationFileUploadCommand, JsonFile>
{
    public OrganizationFileUploadHandler(IAppDbContext context, IFileService file) : base(context, file)
    {
    }

    public async Task<JsonFile> Handle(OrganizationFileUploadCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Organizations.SingleOrDefaultAsync(e => e.OrganizationId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(Organization),
            EntityFiles = entity.Files,
            MaxFiles = 1
        };
        var file = await _file.UploadAsync(command, options, cancellationToken);

        var files = entity.Files ?? new List<JsonFile>();
        entity.Files = files.AddFile(file);

        await _context.SaveChangesAsync(cancellationToken);

        return file;
    }
}

public class OrganizationFileUploadValidator : FileUploadValidator<OrganizationFileUploadCommand>
{
    public OrganizationFileUploadValidator()
    {
    }
}
