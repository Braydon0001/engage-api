namespace Engage.Application.Services.EmployeeAssets.Commands;

public class EmployeeAssetBulkReassignCommand : IRequest<OperationStatus>
{
    public List<int> EmployeeAssetIds { get; set; }
    public int EmployeeId { get; set; }
}

public class EmployeeAssetBulkReassignHandler : IRequestHandler<EmployeeAssetBulkReassignCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;

    public EmployeeAssetBulkReassignHandler(IAppDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<OperationStatus> Handle(EmployeeAssetBulkReassignCommand request, CancellationToken cancellationToken)
    {
        foreach (var id in request.EmployeeAssetIds)
        {
            await _mediator.Send(new EmployeeAssetReassignCommand
            {
                EmployeeAssetId = id,
                EmployeeId = request.EmployeeId,
                SaveChanges = false
            }, cancellationToken);
        }

        return await _context.SaveChangesAsync(cancellationToken);
    }
}

public class EmployeeAssetBulkReassignValidator<T> : AbstractValidator<EmployeeAssetBulkReassignCommand>
{
    public EmployeeAssetBulkReassignValidator()
    {
        RuleFor(x => x.EmployeeId).GreaterThan(0).NotEmpty();
        RuleForEach(x => x.EmployeeAssetIds).GreaterThan(0).NotEmpty();
    }
}