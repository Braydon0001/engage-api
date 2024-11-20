// auto-generated
namespace Engage.Application.Services.ProductTransactions.Commands;

public class ProductTransactionFileUploadCommand : FileUploadCommand, IRequest<JsonFile>
{
}

public class ProductTransactionFileUploadHandler : FileUploadHandler, IRequestHandler<ProductTransactionFileUploadCommand, JsonFile>
{
    public ProductTransactionFileUploadHandler(IAppDbContext context, IFileService file) : base(context, file)
    {
    }

    public async Task<JsonFile> Handle(ProductTransactionFileUploadCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.ProductTransactions.SingleOrDefaultAsync(e => e.ProductTransactionId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(ProductTransaction),
            EntityFiles = entity.Files,
            MaxFiles = 1
        };
        var file = await _file.UploadAsync(command, options, cancellationToken);

         entity.Files = entity.Files.AddFile(file);

        await _context.SaveChangesAsync(cancellationToken);

        return file;
    }
}

public class ProductTransactionFileUploadValidator : FileUploadValidator<ProductTransactionFileUploadCommand>
{
    public ProductTransactionFileUploadValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.File).NotNull();
    }
}