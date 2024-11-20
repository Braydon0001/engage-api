namespace Engage.Application.Services.Manufacturers.Commands;

public class CreateManufacturerCommand: ManufacturerCommand, IRequest<OperationStatus> 
{
    public int SupplierId { get; set; }
}

public class CreateManufacturerCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateManufacturerCommand, OperationStatus>
{
    public CreateManufacturerCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {

    }

    public async Task<OperationStatus> Handle(CreateManufacturerCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateManufacturerCommand, Manufacturer>(request);
        entity.SupplierId = request.SupplierId;

        _context.Manufacturers.Add(entity);

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = entity.ManufacturerId;        
        return operationStatus;
    }
}
