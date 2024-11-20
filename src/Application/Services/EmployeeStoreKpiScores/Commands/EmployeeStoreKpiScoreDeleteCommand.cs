namespace Engage.Application.Services.EmployeeStoreKpiScores.Commands;

public class EmployeeStoreKpiScoreDeleteCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class EmployeeStoreKpiScoreDeleteHandler : IRequestHandler<EmployeeStoreKpiScoreDeleteCommand, OperationStatus>
{
    private readonly IAppDbContext _context;

    public EmployeeStoreKpiScoreDeleteHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<OperationStatus> Handle(EmployeeStoreKpiScoreDeleteCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeStoreKpiScores.SingleOrDefaultAsync(e => e.EmployeeStoreKpiScoreId == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(EmployeeStoreKpiScore), request.Id);

        _context.EmployeeStoreKpiScores.Remove(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = request.Id;
        return opStatus;
    }
}

public class EmployeeStoreKpiScoreDeleteValidator : AbstractValidator<EmployeeStoreKpiScoreDeleteCommand>
{
    public EmployeeStoreKpiScoreDeleteValidator()
    {
        RuleFor(e => e.Id).GreaterThan(0).NotEmpty();
    }
}

