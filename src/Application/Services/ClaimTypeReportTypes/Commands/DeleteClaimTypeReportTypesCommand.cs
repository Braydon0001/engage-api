namespace Engage.Application.Services.ClaimTypeReportTypes.Commands;

public class DeleteClaimTypeReportTypesCommand : IRequest<OperationStatus>
{
    public int ClaimTypeId { get; set; }
    public int ClaimReportTypeId { get; set; }
}

public class DeleteClaimTypeReportTypesCommandHandler : IRequestHandler<DeleteClaimTypeReportTypesCommand, OperationStatus>
{
    private readonly IAppDbContext _context;


    public DeleteClaimTypeReportTypesCommandHandler(IAppDbContext context)
    {
        this._context = context;
    }

    public async Task<OperationStatus> Handle(DeleteClaimTypeReportTypesCommand request, CancellationToken cancellationToken)
    {
        var existing = await _context.ClaimTypeReportTypes.IgnoreQueryFilters()
                                                             .Where(e => e.ClaimTypeId == request.ClaimTypeId &&
                                                                         e.ClaimReportTypeId == request.ClaimReportTypeId)
                                                             .ToListAsync(cancellationToken);

        if(existing.Count > 0)
        {
            foreach(var record in existing)
            {
                _context.ClaimTypeReportTypes.Remove(record);
            }

            return await _context.SaveChangesAsync(cancellationToken);
        }

        throw new ClaimException("Delete Action Failed. \n\n Could not find record to delete.");

    }
}
