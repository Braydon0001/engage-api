namespace Engage.Application.Services.EmployeeStores.Commands;

public class EmployeeStoreDeleteCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class EmployeeStoreDeleteHandler : IRequestHandler<EmployeeStoreDeleteCommand, OperationStatus>
{
    private readonly IAppDbContext _context;

    public EmployeeStoreDeleteHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<OperationStatus> Handle(EmployeeStoreDeleteCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeStores.SingleOrDefaultAsync(e => e.EmployeeStoreId == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(SupplierStore), request.Id);

        _context.EmployeeStores.Remove(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = request.Id;
        return opStatus;
    }
}

public class EmployeeStoreDeleteValidator : AbstractValidator<EmployeeStoreDeleteCommand>
{
    public EmployeeStoreDeleteValidator()
    {
        RuleFor(e => e.Id).GreaterThan(0).NotEmpty();
    }
}

