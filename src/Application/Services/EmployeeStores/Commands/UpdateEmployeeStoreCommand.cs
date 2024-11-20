namespace Engage.Application.Services.EmployeeStores.Commands;

public class UpdateEmployeeStoreCommand : IRequest<OperationStatus>, IMapTo<EmployeeStore>
{
    public int Id { get; set; }
    public int FrequencyTypeId { get; set; }
    public int Frequency { get; set; }
    public string Note { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateEmployeeStoreCommand, EmployeeStore>();
    }
}

public class UpdateEmployeeStoreCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateEmployeeStoreCommand, OperationStatus>
{
    public UpdateEmployeeStoreCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(UpdateEmployeeStoreCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeStores.SingleAsync(x => x.EmployeeStoreId == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EmployeeStoreId;
        return opStatus;
    }
}
