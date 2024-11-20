namespace Engage.Application.Services.EmployeeAssets.Commands;

public class EmployeeAssetReassignCommand : IRequest<OperationStatus>
{
    public int EmployeeAssetId { get; set; }
    public int EmployeeId { get; set; }
    public bool SaveChanges { get; set; } = true;
}

public class EmployeeAssetReassignHandler : IRequestHandler<EmployeeAssetReassignCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;

    public EmployeeAssetReassignHandler(IAppDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<OperationStatus> Handle(EmployeeAssetReassignCommand request, CancellationToken cancellationToken)
    {
        var asset = await _context.EmployeeAssets.SingleOrDefaultAsync(e => e.EmployeeAssetId == request.EmployeeAssetId, cancellationToken);
        if (asset == null)
        {
            throw new NotFoundException(nameof(EmployeeAsset), request.EmployeeAssetId);
        }

        if (asset.EmployeeId == request.EmployeeId)
        {
            return new OperationStatus(true);
        }

        var history = new EmployeeAssetHistory
        {
            EmployeeAssetId = asset.EmployeeAssetId,
            OldEmployeeId = asset.EmployeeId,
            NewEmployeeId = request.EmployeeId,
        };

        _context.EmployeeAssetHistories.Add(history);
        //_context.EmployeeAssets.Remove(asset);
        asset.EmployeeId = request.EmployeeId;

        return await _context.SaveChangesAsync(cancellationToken);

        //return await _mediator.Send(new EmployeeAssetCreateCommand
        //{
        //    EmployeeId = request.EmployeeId,
        //    EmployeeAssetTypeId = asset.EmployeeAssetTypeId,
        //    EmployeeAssetBrandId = asset.EmployeeAssetBrandId,
        //    AssetStatusId = asset.AssetStatusId,
        //    Name = asset.Name,
        //    Description = asset.Description,
        //    Contract = asset.Contract,
        //    MobileNumber = asset.MobileNumber,
        //    Sim = asset.Sim,
        //    Imei = asset.Imei,
        //    SerialNumber = asset.SerialNumber,
        //    Note = asset.Note,
        //    SaveChanges = request.SaveChanges
        //}, cancellationToken);



    }
}
