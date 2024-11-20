using Engage.Application.Services.Mobile.Database.Models;

namespace Engage.Application.Services.Mobile.Database.Queries;

public class GetStoresDCsByUserIdQuery : IRequest<List<StoreDCDto>>
{
    public int EmployeeId { get; set; }
}

public class GetStoresDCsByUserIdQueryHandler : BaseQueryHandler, IRequestHandler<GetStoresDCsByUserIdQuery, List<StoreDCDto>>
{
    IMediator _mediator;
    public GetStoresDCsByUserIdQueryHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper)
    {
        _mediator = mediator;
    }

    public async Task<List<StoreDCDto>> Handle(GetStoresDCsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var stores = await _mediator.Send(new GetStoresByUserIdQuery() { EmployeeId = request.EmployeeId });
        var storeIds = stores.Select(s => s.Id);


        var result = await _context.DCAccounts
            .Where(d => storeIds.Contains(d.StoreId))
            .Select(d => new StoreDCDto
            {
                StoreId = d.StoreId,
                DCId = d.DistributionCenterId,
                DCName = d.DistributionCenter.Name,
                AccountNumber = d.AccountNumber
            }).ToListAsync();

        return result;

    }
}
