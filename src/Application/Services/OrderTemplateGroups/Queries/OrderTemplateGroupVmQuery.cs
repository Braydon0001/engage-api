// auto-generated
namespace Engage.Application.Services.OrderTemplateGroups.Queries;

public class OrderTemplateGroupVmQuery : IRequest<OrderTemplateGroupVm>
{
    public int Id { get; set; }
}

public class OrderTemplateGroupVmHandler : VmQueryHandler, IRequestHandler<OrderTemplateGroupVmQuery, OrderTemplateGroupVm>
{
    public OrderTemplateGroupVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OrderTemplateGroupVm> Handle(OrderTemplateGroupVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.OrderTemplateGroups.AsQueryable().AsNoTracking();

        var entity = await queryable.Include(e => e.OrderTemplateProducts)
                                    .SingleOrDefaultAsync(e => e.OrderTemplateGroupId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<OrderTemplateGroupVm>(entity);
    }
}