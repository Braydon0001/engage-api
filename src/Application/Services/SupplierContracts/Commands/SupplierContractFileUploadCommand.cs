// auto-generated
namespace Engage.Application.Services.SupplierContracts.Commands;

public class SupplierContractFileUploadCommand : FileUploadCommand, IRequest<JsonFile>
{
}

public class SupplierContractFileUploadHandler : FileUploadHandler, IRequestHandler<SupplierContractFileUploadCommand, JsonFile>
{
    public SupplierContractFileUploadHandler(IAppDbContext context, IFileService file) : base(context, file)
    {
    }

    public async Task<JsonFile> Handle(SupplierContractFileUploadCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SupplierContracts.SingleOrDefaultAsync(e => e.SupplierContractId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(SupplierContract),
            EntityFiles = entity.Files,
            MaxFiles = 3
        };
        var file = await _file.UploadAsync(command, options, cancellationToken);

        entity.Files = entity.Files.AddFile(file);

        await _context.SaveChangesAsync(cancellationToken);

        return file;
    }
}

public class SupplierContractFileUploadValidator : FileUploadValidator<SupplierContractFileUploadCommand>
{
    public SupplierContractFileUploadValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.File).NotNull();
    }
}