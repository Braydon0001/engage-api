namespace Engage.Application.Services.ProductOrders.Commands;

public class ProductOrderFileUploadCommand : FileUploadCommand, IRequest<JsonFile>
{
}

public record ProductOrderFileUploadHandler(IAppDbContext Context, IFileService File) : IRequestHandler<ProductOrderFileUploadCommand, JsonFile>
{
    public async Task<JsonFile> Handle(ProductOrderFileUploadCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProductOrders.SingleOrDefaultAsync(e => e.ProductOrderId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(ProductOrder),
            EntityFiles = entity.Files,
            MaxFiles = 1
        };
        var file = await File.UploadAsync(command, options, cancellationToken);

        entity.Files = entity.Files.AddFile(file);

        await Context.SaveChangesAsync(cancellationToken);

        return file;
    }
}

public class ProductOrderFileUploadValidator : FileUploadValidator<ProductOrderFileUploadCommand>
{
    public ProductOrderFileUploadValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.File).NotNull();
    }
}