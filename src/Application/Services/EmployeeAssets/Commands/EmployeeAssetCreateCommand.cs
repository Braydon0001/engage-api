namespace Engage.Application.Services.EmployeeAssets.Commands;

public class EmployeeAssetCreateCommand : EmployeeAssetCommand, IMapTo<EmployeeAsset>, IRequest<OperationStatus>
{
    public bool SaveChanges { get; set; } = true;

    public void Mapping(Profile profile)
    {

        profile.CreateMap<EmployeeAssetCommand, EmployeeAsset>();
    }
}

public class EmployeeAssetCreateHandler : BaseCreateCommandHandler, IRequestHandler<EmployeeAssetCreateCommand, OperationStatus>
{
    public EmployeeAssetCreateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(EmployeeAssetCreateCommand command, CancellationToken cancellationToken)
    {
        var existingMobileClaimNumber = await _context.EmployeeAssets
                                                .Where(c => c.MobileNumber == command.MobileNumber && c.EmployeeId == command.EmployeeId
                                                    && c.Deleted == false)
                                                .FirstOrDefaultAsync();

        var assetType = await _context.EmployeeAssetTypes
                                .Where(c => c.Id == command.EmployeeAssetTypeId)
                                .FirstOrDefaultAsync();

        if (assetType.Name != "Laptop" && existingMobileClaimNumber != null)
        {
            throw new UniqueException("This Mobile Number Already Exists. \n\n It can't be added again.");
        }

        var entity = _mapper.Map<EmployeeAssetCreateCommand, EmployeeAsset>(command);
        _context.EmployeeAssets.Add(entity);

        if (command.SaveChanges)
        {
            var opStatus = await _context.SaveChangesAsync(cancellationToken);
            opStatus.OperationId = entity.EmployeeAssetId;
            return opStatus;
        }

        return new OperationStatus(true);
    }
}

public class EmployeeAssetCreateValidator : EmployeeAssetValidator<EmployeeAssetCreateCommand>
{
    public EmployeeAssetCreateValidator()
    {
    }
}