// auto-generated
namespace Engage.Application.Services.OrderTemplateStatuses.Queries;

public class OrderTemplateStatusVmQuery : IRequest<OrderTemplateStatusVm>
{
    public int Id { get; set; }
}

public class OrderTemplateStatusVmHandler : VmQueryHandler, IRequestHandler<OrderTemplateStatusVmQuery, OrderTemplateStatusVm>
{
    public OrderTemplateStatusVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OrderTemplateStatusVm> Handle(OrderTemplateStatusVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.OrderTemplateStatuses.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.OrderTemplateStatusId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<OrderTemplateStatusVm>(entity);
    }
}