namespace Engage.Application.Services.ClaimTypeReportTypes.Commands;

public class CreateClaimTypeReportTypesCommand : IRequest<OperationStatus>
{
    public int ClaimTypeId { get; set; }
    public int ClaimReportTypeId { get; set; }
}

public class CreateClaimTypeReportTypesCommandHandler : IRequestHandler<CreateClaimTypeReportTypesCommand, OperationStatus>
{
    private readonly IAppDbContext _context;


    public CreateClaimTypeReportTypesCommandHandler(IAppDbContext context)
    {
        this._context = context;
    }

    public async Task<OperationStatus> Handle(CreateClaimTypeReportTypesCommand request, CancellationToken cancellationToken)
    {
        var existing = await _context.ClaimTypeReportTypes.IgnoreQueryFilters()
                                                             .Where(e => e.ClaimTypeId == request.ClaimTypeId &&
                                                                         e.ClaimReportTypeId == request.ClaimReportTypeId)
                                                             .ToListAsync(cancellationToken);

        if(existing.Count > 0)
        {
            throw new ClaimException("This Report Type already has this Claim Type. \n\n It can't be added again.");
        }

        _context.ClaimTypeReportTypes.Add(new ClaimTypeReportType
        {
            ClaimTypeId = request.ClaimTypeId,
            ClaimReportTypeId = request.ClaimReportTypeId
        });

        return await _context.SaveChangesAsync(cancellationToken);
    }
}
