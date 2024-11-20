namespace Engage.Application.Services.EmployeeAssets.Commands;

public class EmployeeAssetUpdateCommand : EmployeeAssetCommand, IMapTo<EmployeeAsset>, IRequest<OperationStatus>
{
    public int Id { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeAssetCommand, EmployeeAsset>();
    }
}

public class UpdateAssetCommandHandler : BaseUpdateCommandHandler, IRequestHandler<EmployeeAssetUpdateCommand, OperationStatus>
{
    public UpdateAssetCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(EmployeeAssetUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeAssets.SingleAsync(x => x.EmployeeAssetId == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EmployeeAssetId;
        return opStatus;
    }
}

public class UpdateEmployeeAssetValidator : EmployeeAssetValidator<EmployeeAssetUpdateCommand>
{
    public UpdateEmployeeAssetValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}