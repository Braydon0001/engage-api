namespace Engage.Application.Services.SupplierContractDetails.Commands;

public class SupplierContractDetailDeleteCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class SupplierContractDetailDeleteHandler : IRequestHandler<SupplierContractDetailDeleteCommand, OperationStatus>
{
    private readonly IAppDbContext _context;

    public SupplierContractDetailDeleteHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<OperationStatus> Handle(SupplierContractDetailDeleteCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.SupplierContractDetails.SingleOrDefaultAsync(e => e.SupplierContractDetailId == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(SupplierStore), request.Id);

        _context.SupplierContractDetails.Remove(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = request.Id;
        return opStatus;
    }
}

public class SupplierContractDetailDeleteValidator : AbstractValidator<SupplierContractDetailDeleteCommand>
{
    public SupplierContractDetailDeleteValidator()
    {
        RuleFor(e => e.Id).GreaterThan(0).NotEmpty();
    }
}

