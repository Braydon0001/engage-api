namespace Engage.Application.Services.ProductMasters.Commands;

public class ProductMasterFileDeleteCommand : FileDeleteCommand, IRequest<bool>
{
}
public class ProductMasterFileDeleteHandler : FileDeleteHandler, IRequestHandler<ProductMasterFileDeleteCommand, bool>
{
    public ProductMasterFileDeleteHandler(IAppDbContext context, IFileService file) : base(context, file)
    {
    }

    public async Task<bool> Handle(ProductMasterFileDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.ProductMasters.SingleOrDefaultAsync(e => e.ProductMasterId == command.Id, cancellationToken);
        if (entity == null || entity.Files == null || !entity.Files.FileExists(command))
        {
            return false;
        }

        await _file.DeleteAsync(command, nameof(ProductMaster), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}

public class ProductMasterFileDeleteValidator : FileDeleteValidator<ProductMasterFileDeleteCommand>
{
    public ProductMasterFileDeleteValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
    }
}