// auto-generated
namespace Engage.Application.Services.SupplierContractDetails.Commands;

public class SupplierContractDetailFileDeleteCommand : FileDeleteCommand, IRequest<bool>
{
}

public class SupplierContractDetailFileDeleteHandler : FileDeleteHandler, IRequestHandler<SupplierContractDetailFileDeleteCommand, bool>
{
    public SupplierContractDetailFileDeleteHandler(IAppDbContext context, IFileService file) : base(context, file)
    {
    }

    public async Task<bool> Handle(SupplierContractDetailFileDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SupplierContractDetails.SingleOrDefaultAsync(e => e.SupplierContractDetailId == command.Id, cancellationToken);
        if (entity == null || entity.Files == null || !entity.Files.FileExists(command))
        {
            return false;
        }

        await _file.DeleteAsync(command, nameof(SupplierContractDetail), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);
        
        await _context.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}

public class SupplierContractDetailFileDeleteValidator : FileDeleteValidator<SupplierContractDetailFileDeleteCommand>
{
    public SupplierContractDetailFileDeleteValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
    }
}