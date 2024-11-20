namespace Engage.Application.Services.SupplierClaimAccountManagers.Commands;

public class CreateSupplierClaimAccountManagersCommand : IRequest<OperationStatus>
{
    public int SupplierId { get; set; }

    public List<int> UserIds { get; set; }
}

public class CreateSupplierClaimAccountManagersCommandHandler : IRequestHandler<CreateSupplierClaimAccountManagersCommand, OperationStatus>
{
    private readonly IAppDbContext _context;


    public CreateSupplierClaimAccountManagersCommandHandler(IAppDbContext context)
    {
        this._context = context;
    }

    public async Task<OperationStatus> Handle(CreateSupplierClaimAccountManagersCommand request, CancellationToken cancellationToken)
    {
        var ids = await _context.SupplierClaimAccountManagers.IgnoreQueryFilters()
                                                             .Where(e => e.SupplierId == request.SupplierId &&
                                                                         request.UserIds.Contains(e.UserId))
                                                             .Select(e => e.UserId)        
                                                             .ToListAsync(cancellationToken);

        var newIds = request.UserIds.Except(ids);

        foreach (var id in newIds)
        {
            _context.SupplierClaimAccountManagers.Add(new SupplierClaimAccountManager { 
               SupplierId = request.SupplierId,
               UserId = id
            });
        }

        return await _context.SaveChangesAsync(cancellationToken);
    }
}

