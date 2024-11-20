using Engage.Application.Services.Orders.Models;

namespace Engage.Application.Services.Orders.Queries;

public class OrderEmailVmQuery : GetByIdQuery, IRequest<OrderEmailVm>
{
}
public class OrderEmailVmQueryHandler : BaseQueryHandler, IRequestHandler<OrderEmailVmQuery, OrderEmailVm>
{
    private readonly ContactReportSettings _contactReportSettings;
    public OrderEmailVmQueryHandler(IAppDbContext context, IMapper mapper, IOptions<ContactReportSettings> contactReportSettings) : base(context, mapper)
    {
        _contactReportSettings = contactReportSettings.Value;
    }

    public async Task<OrderEmailVm> Handle(OrderEmailVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Orders.IgnoreQueryFilters()
                                          .Include(x => x.Store)
                                          .SingleAsync(x => x.OrderId == request.Id, cancellationToken);

        var currentUser = await _context.Users.Where(e => e.Email == entity.CreatedBy)
                                                      .FirstOrDefaultAsync(cancellationToken);

        var vm = _mapper.Map<Order, OrderEmailVm>(entity);

        vm.EngageLogo = _contactReportSettings.EngageLogo;

        if (currentUser != null)
        {
            vm.OrderPlacedBy = currentUser.FirstName + " " + currentUser.LastName;
        }
        else
        {
            vm.OrderPlacedBy = entity.CreatedBy;
        }

        return vm;
    }
}