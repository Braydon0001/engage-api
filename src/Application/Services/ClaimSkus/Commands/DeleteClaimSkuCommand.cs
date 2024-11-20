namespace Engage.Application.Services.ClaimSkus.Commands;

public class DeleteClaimSkuCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class DeleteClaimSkuCommandHandler : BaseCreateCommandHandler, IRequestHandler<DeleteClaimSkuCommand, OperationStatus>
{
    public DeleteClaimSkuCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(DeleteClaimSkuCommand command, CancellationToken cancellationToken)
    {
        var record = await _context.ClaimSkus.IgnoreQueryFilters()
                                                    .Where(e => e.ClaimSkuId == command.Id)
                                                    .FirstOrDefaultAsync(cancellationToken);

        if (record != null)
        {
            _context.ClaimSkus.Remove(record);

            return await _context.SaveChangesAsync(cancellationToken);
        }

        throw new ClaimException("Delete Action Failed. \n\n Could not find record to delete.");
    }
}
