// auto-generated
namespace Engage.Application.Services.SupplierContracts.Commands;

public class SupplierContractFileDeleteCommand : FileDeleteCommand, IRequest<bool>
{
}

public class SupplierContractFileDeleteHandler : FileDeleteHandler, IRequestHandler<SupplierContractFileDeleteCommand, bool>
{
    public SupplierContractFileDeleteHandler(IAppDbContext context, IFileService file) : base(context, file)
    {
    }

    public async Task<bool> Handle(SupplierContractFileDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SupplierContracts.SingleOrDefaultAsync(e => e.SupplierContractId == command.Id, cancellationToken);
        if (entity == null || entity.Files == null || !entity.Files.FileExists(command))
        {
            return false;
        }

        await _file.DeleteAsync(command, nameof(SupplierContract), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);
        
        await _context.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}

public class SupplierContractFileDeleteValidator : FileDeleteValidator<SupplierContractFileDeleteCommand>
{
    public SupplierContractFileDeleteValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
    }
}