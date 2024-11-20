namespace Engage.Application.Services.VoucherDetails.Commands;

public class DeleteVoucherDetailCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class DeleteVoucherDetailCommandHandler : BaseCreateCommandHandler, IRequestHandler<DeleteVoucherDetailCommand, OperationStatus>
{
    public DeleteVoucherDetailCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(DeleteVoucherDetailCommand command, CancellationToken cancellationToken)
    {
        var record = await _context.VoucherDetails.IgnoreQueryFilters()
                                                    .Where(e => e.VoucherDetailId == command.Id)
                                                    .FirstOrDefaultAsync(cancellationToken);

        if (record != null)
        {
            _context.VoucherDetails.Remove(record);

            return await _context.SaveChangesAsync(cancellationToken);
        }

        throw new ClaimException("Delete Action Failed. \n\n Could not find record to delete.");
    }
}
