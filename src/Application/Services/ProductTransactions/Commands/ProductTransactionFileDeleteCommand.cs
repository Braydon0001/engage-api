// auto-generated
namespace Engage.Application.Services.ProductTransactions.Commands;

public class ProductTransactionFileDeleteCommand : FileDeleteCommand, IRequest<bool>
{
}

public class ProductTransactionFileDeleteHandler : FileDeleteHandler, IRequestHandler<ProductTransactionFileDeleteCommand, bool>
{
    public ProductTransactionFileDeleteHandler(IAppDbContext context, IFileService file) : base(context, file)
    {
    }

    public async Task<bool> Handle(ProductTransactionFileDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.ProductTransactions.SingleOrDefaultAsync(e => e.ProductTransactionId == command.Id, cancellationToken);
        if (entity == null || entity.Files == null || !entity.Files.FileExists(command))
        {
            return false;
        }

        await _file.DeleteAsync(command, nameof(ProductTransaction), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);
        
        await _context.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}

public class ProductTransactionFileDeleteValidator : FileDeleteValidator<ProductTransactionFileDeleteCommand>
{
    public ProductTransactionFileDeleteValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
    }
}