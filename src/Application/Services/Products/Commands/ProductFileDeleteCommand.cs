// auto-generated
namespace Engage.Application.Services.Products.Commands;

public class ProductFileDeleteCommand : FileDeleteCommand, IRequest<bool>
{
}

public class ProductFileDeleteHandler : FileDeleteHandler, IRequestHandler<ProductFileDeleteCommand, bool>
{
    public ProductFileDeleteHandler(IAppDbContext context, IFileService file) : base(context, file)
    {
    }

    public async Task<bool> Handle(ProductFileDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Products.SingleOrDefaultAsync(e => e.ProductId == command.Id, cancellationToken);
        if (entity == null || entity.Files == null || !entity.Files.FileExists(command))
        {
            return false;
        }

        await _file.DeleteAsync(command, nameof(Product), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);
        
        await _context.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}

public class ProductFileDeleteValidator : FileDeleteValidator<ProductFileDeleteCommand>
{
    public ProductFileDeleteValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
    }
}