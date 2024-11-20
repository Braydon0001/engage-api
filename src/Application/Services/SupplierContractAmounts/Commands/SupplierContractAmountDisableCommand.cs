namespace Engage.Application.Services.SupplierContractAmounts.Commands;

public class SupplierContractAmountDisableCommand : IRequest<SupplierContractAmount>
{
    public int Id { get; set; }
}
public class SupplierContractAmountDisableHandler : IRequestHandler<SupplierContractAmountDisableCommand, SupplierContractAmount>
{
    private readonly IAppDbContext _context;
    public SupplierContractAmountDisableHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<SupplierContractAmount> Handle(SupplierContractAmountDisableCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.SupplierContractAmounts.SingleOrDefaultAsync(e => e.SupplierContractAmountId == request.Id, cancellationToken);
        if (entity == null)
            throw new NotFoundException("Sub-Contract Amount not found", entity);

        entity.Disabled = true;

        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }
}
