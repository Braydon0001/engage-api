namespace Engage.Application.Services.StoreBankDetails.Commands;

public class CreateStoreBankDetailCommand : StoreBankDetailCommand, IRequest<OperationStatus>
{
    public int StoreId { get; set; }
}

public class CreateStoreBankDetailCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateStoreBankDetailCommand, OperationStatus>
{
    public CreateStoreBankDetailCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(CreateStoreBankDetailCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateStoreBankDetailCommand, StoreBankDetail>(command);
        entity.StoreId = command.StoreId;
        _context.StoreBankDetails.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.StoreBankDetailId;
        return opStatus;
    }
}
