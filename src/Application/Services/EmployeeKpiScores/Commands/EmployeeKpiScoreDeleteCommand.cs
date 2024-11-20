namespace Engage.Application.Services.EmployeeKpiScores.Commands;

public class EmployeeKpiScoreDeleteCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class EmployeeKpiScoreDeleteHandler : IRequestHandler<EmployeeKpiScoreDeleteCommand, OperationStatus>
{
    private readonly IAppDbContext _context;

    public EmployeeKpiScoreDeleteHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<OperationStatus> Handle(EmployeeKpiScoreDeleteCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeKpiScores.SingleOrDefaultAsync(e => e.EmployeeKpiScoreId == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(EmployeeKpiScore), request.Id);

        _context.EmployeeKpiScores.Remove(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = request.Id;
        return opStatus;
    }
}

public class EmployeeKpiScoreDeleteValidator : AbstractValidator<EmployeeKpiScoreDeleteCommand>
{
    public EmployeeKpiScoreDeleteValidator()
    {
        RuleFor(e => e.Id).GreaterThan(0).NotEmpty();
    }
}

