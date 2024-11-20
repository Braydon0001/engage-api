// auto-generated
namespace Engage.Application.Services.SupplierContractDetails.Commands;

public class SupplierContractDetailFileUploadCommand : FileUploadCommand, IRequest<JsonFile>
{
}

public class SupplierContractDetailFileUploadHandler : FileUploadHandler, IRequestHandler<SupplierContractDetailFileUploadCommand, JsonFile>
{
    public SupplierContractDetailFileUploadHandler(IAppDbContext context, IFileService file) : base(context, file)
    {
    }

    public async Task<JsonFile> Handle(SupplierContractDetailFileUploadCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SupplierContractDetails.SingleOrDefaultAsync(e => e.SupplierContractDetailId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(SupplierContractDetail),
            EntityFiles = entity.Files,
            MaxFiles = 2
        };
        var file = await _file.UploadAsync(command, options, cancellationToken);

        entity.Files = entity.Files.AddFile(file);

        await _context.SaveChangesAsync(cancellationToken);

        return file;
    }
}

public class SupplierContractDetailFileUploadValidator : FileUploadValidator<SupplierContractDetailFileUploadCommand>
{
    public SupplierContractDetailFileUploadValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.File).NotNull();
    }
}