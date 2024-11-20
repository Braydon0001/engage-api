// auto-generated
namespace Engage.Application.Services.Products.Commands;

public class ProductFileUploadCommand : FileUploadCommand, IRequest<JsonFile>
{
}

public class ProductFileUploadHandler : FileUploadHandler, IRequestHandler<ProductFileUploadCommand, JsonFile>
{
    public ProductFileUploadHandler(IAppDbContext context, IFileService file) : base(context, file)
    {
    }

    public async Task<JsonFile> Handle(ProductFileUploadCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Products.SingleOrDefaultAsync(e => e.ProductId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(Product),
            EntityFiles = entity.Files,
            MaxFiles = 1
        };
        var file = await _file.UploadAsync(command, options, cancellationToken);

         entity.Files = entity.Files.AddFile(file);

        await _context.SaveChangesAsync(cancellationToken);

        return file;
    }
}

public class ProductFileUploadValidator : FileUploadValidator<ProductFileUploadCommand>
{
    public ProductFileUploadValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.File).NotNull();
    }
}