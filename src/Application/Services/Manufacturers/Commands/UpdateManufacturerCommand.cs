namespace Engage.Application.Services.Manufacturers.Commands;

public class UpdateManufacturerCommand: ManufacturerCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateManufacturerCommandHandler: BaseUpdateCommandHandler, IRequestHandler<UpdateManufacturerCommand, OperationStatus>
{
    public UpdateManufacturerCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(UpdateManufacturerCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Manufacturers.SingleAsync(e => e.ManufacturerId == request.Id, cancellationToken);
        _mapper.Map(request, entity);

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = entity.ManufacturerId;
        return operationStatus;

    }
}
