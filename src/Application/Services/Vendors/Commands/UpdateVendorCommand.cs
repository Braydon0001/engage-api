namespace Engage.Application.Services.Vendors.Commands;

public class UpdateVendorCommand : VendorCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateVendorCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateVendorCommand, OperationStatus>
{
    public UpdateVendorCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(UpdateVendorCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Vendors.SingleAsync(x => x.VendorId == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = entity.VendorId;
        return operationStatus;
    }
}
