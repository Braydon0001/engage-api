namespace Engage.Application.Services.ProductMasters.Commands;

public class ProductMasterFileUploadCommand : FileUploadCommand, IRequest<JsonFile>
{
}

public class ProductMasterFileUploadHandler : FileUploadHandler, IRequestHandler<ProductMasterFileUploadCommand, JsonFile>
{
    public ProductMasterFileUploadHandler(IAppDbContext context, IFileService file) : base(context, file)
    {
    }

    public async Task<JsonFile> Handle(ProductMasterFileUploadCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.ProductMasters.SingleOrDefaultAsync(e => e.ProductMasterId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(ProductMaster),
            EntityFiles = entity.Files,
            MaxFiles = 1
        };
        var file = await _file.UploadAsync(command, options, cancellationToken);

        entity.Files = entity.Files.AddFile(file);

        await _context.SaveChangesAsync(cancellationToken);

        return file;
    }
}

public class ProductMasterFileUplaodValidator : FileUploadValidator<ProductMasterFileUploadCommand>
{
    public ProductMasterFileUplaodValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.File).NotNull();
    }
}