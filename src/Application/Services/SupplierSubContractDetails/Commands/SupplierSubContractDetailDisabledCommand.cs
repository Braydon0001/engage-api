namespace Engage.Application.Services.SupplierSubContractDetails.Commands;

public class SupplierSubContractDetailDisabledCommand : IRequest<SupplierSubContractDetail>
{
    public int Id { get; set; }
}
public class SupplierSubContractDetailDisabledHandler : IRequestHandler<SupplierSubContractDetailDisabledCommand, SupplierSubContractDetail>
{
    private readonly IAppDbContext _context;

    public SupplierSubContractDetailDisabledHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<SupplierSubContractDetail> Handle(SupplierSubContractDetailDisabledCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.SupplierSubContractDetails.SingleOrDefaultAsync(e => e.SupplierSubContractDetailId == request.Id, cancellationToken);
        if (entity == null)
            throw new NotFoundException("Sub-Contract Detail not found", entity);

        entity.Disabled = true;

        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }
}

public class SupplierSubContractDetailDisabledValidator : AbstractValidator<SupplierSubContractDetailDisabledCommand>
{
    public SupplierSubContractDetailDisabledValidator()
    {
        RuleFor(e => e.Id).GreaterThan(0).NotEmpty();
    }
}