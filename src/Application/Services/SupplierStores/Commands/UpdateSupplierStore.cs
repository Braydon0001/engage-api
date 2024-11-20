namespace Engage.Application.Services.SupplierStores.Commands;

public class UpdateSupplierStoreCommand : IRequest<OperationStatus>, IMapTo<SupplierStore>
{
    public int Id { get; set; }
    public int SupplierRegionId { get; set; }
    public int? SupplierSubRegionId { get; set; }
    public string AccountNumber { get; set; }
    public int FrequencyTypeId { get; set; }
    public int Frequency { get; set; }
    public string Note { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateSupplierStoreCommand, SupplierStore>();
    }
}

public class UpdateSupplierStoreCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateSupplierStoreCommand, OperationStatus>
{
    public UpdateSupplierStoreCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(UpdateSupplierStoreCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SupplierStores.SingleAsync(x => x.SupplierStoreId == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.SupplierStoreId;
        return opStatus;
    }
}