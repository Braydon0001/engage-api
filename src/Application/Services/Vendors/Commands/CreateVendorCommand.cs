namespace Engage.Application.Services.Vendors.Commands;

public class CreateVendorCommand : VendorCommand, IRequest<OperationStatus>
{
    
}

public class CreateVendorCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateVendorCommand, OperationStatus>
{
    public CreateVendorCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(CreateVendorCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateVendorCommand, Vendor>(command);        
        _context.Vendors.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.VendorId;
        return opStatus;
    }
}
