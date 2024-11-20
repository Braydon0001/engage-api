namespace Engage.Application.Services.SupplierSubContracts.Commands;

public class SupplierSubContractDisableCommand : IRequest<SupplierSubContract>
{
    public int Id { get; set; }
}
public class SupplierSubContractDisableHandler : IRequestHandler<SupplierSubContractDisableCommand, SupplierSubContract>
{
    private readonly IAppDbContext _context;
    public SupplierSubContractDisableHandler(IAppDbContext context)
    {
        _context = context;
    }
    public async Task<SupplierSubContract> Handle(SupplierSubContractDisableCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.SupplierSubContracts.SingleOrDefaultAsync(e => e.SupplierSubContractId == request.Id, cancellationToken);
        if (entity == null)
            throw new NotFoundException("Sub-Contract not found", entity);

        entity.Disabled = true;

        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }
}

public class SupplierSubContractDisableValidator : AbstractValidator<SupplierSubContractDisableCommand>
{
    public SupplierSubContractDisableValidator()
    {
        RuleFor(e => e.Id).GreaterThan(0).NotEmpty();
    }
}