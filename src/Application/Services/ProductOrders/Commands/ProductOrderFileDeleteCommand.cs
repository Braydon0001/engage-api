namespace Engage.Application.Services.ProductOrders.Commands;

public class ProductOrderFileDeleteCommand : FileDeleteCommand, IRequest<bool>
{
}

public record ProductOrderFileDeleteHandler(IAppDbContext Context, IFileService File) : IRequestHandler<ProductOrderFileDeleteCommand, bool>
{
    public async Task<bool> Handle(ProductOrderFileDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProductOrders.SingleOrDefaultAsync(e => e.ProductOrderId == command.Id, cancellationToken);
        if (entity == null || entity.Files == null || !entity.Files.FileExists(command))
        {
            return false;
        }

        await File.DeleteAsync(command, nameof(ProductOrder), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);
        
        await Context.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}

public class ProductOrderFileDeleteValidator : FileDeleteValidator<ProductOrderFileDeleteCommand>
{
    public ProductOrderFileDeleteValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
    }
}