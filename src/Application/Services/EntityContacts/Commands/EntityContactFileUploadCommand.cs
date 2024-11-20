namespace Engage.Application.Services.EntityContacts.Commands;

public class StoreContactFileUploadCommand : FileUploadCommand, IRequest<JsonFile>
{
}

public class StoreContactFileUploadHandler : FileUploadHandler, IRequestHandler<StoreContactFileUploadCommand, JsonFile>
{
    public StoreContactFileUploadHandler(IAppDbContext context, IFileService file) : base(context, file)
    {
    }

    public async Task<JsonFile> Handle(StoreContactFileUploadCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.StoreContacts.SingleOrDefaultAsync(e => e.EntityContactId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(StoreContact),
            EntityFiles = entity.Files,
            MaxFiles = 1
        };
        var file = await _file.UploadAsync(command, options, cancellationToken);

        entity.Files = entity.Files.AddFile(file);

        await _context.SaveChangesAsync(cancellationToken);

        return file;
    }
}

public class StoreContactFileUploadValidator : FileUploadValidator<StoreContactFileUploadCommand>
{
    public StoreContactFileUploadValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.File).NotNull();
    }
}