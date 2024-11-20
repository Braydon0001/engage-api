namespace Engage.Application.Services.ProductOrderLines.Commands;

public class ProductOrderLineFileDeleteCommand : FileDeleteCommand, IRequest<bool>
{
}

public record ProductOrderLineFileDeleteHandler(IAppDbContext Context, IFileService File) : IRequestHandler<ProductOrderLineFileDeleteCommand, bool>
{
    public async Task<bool> Handle(ProductOrderLineFileDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProductOrderLines.SingleOrDefaultAsync(e => e.ProductOrderLineId == command.Id, cancellationToken);
        if (entity == null || entity.Files == null || !entity.Files.FileExists(command))
        {
            return false;
        }

        await File.DeleteAsync(command, nameof(ProductOrderLine), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);
        
        await Context.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}

public class ProductOrderLineFileDeleteValidator : FileDeleteValidator<ProductOrderLineFileDeleteCommand>
{
    public ProductOrderLineFileDeleteValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
    }
}