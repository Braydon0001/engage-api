namespace Engage.Application.Services.ProductOrderLines.Commands;

public class ProductOrderLineFileUploadCommand : FileUploadCommand, IRequest<JsonFile>
{
}

public record ProductOrderLineFileUploadHandler(IAppDbContext Context, IFileService File) : IRequestHandler<ProductOrderLineFileUploadCommand, JsonFile>
{
    public async Task<JsonFile> Handle(ProductOrderLineFileUploadCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProductOrderLines.SingleOrDefaultAsync(e => e.ProductOrderLineId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(ProductOrderLine),
            EntityFiles = entity.Files,
            MaxFiles = 1
        };
        var file = await File.UploadAsync(command, options, cancellationToken);

        entity.Files = entity.Files.AddFile(file);

        await Context.SaveChangesAsync(cancellationToken);

        return file;
    }
}

public class ProductOrderLineFileUploadValidator : FileUploadValidator<ProductOrderLineFileUploadCommand>
{
    public ProductOrderLineFileUploadValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.File).NotNull();
    }
}