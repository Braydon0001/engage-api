namespace Engage.Application.Services.OrderStagings.Queries;

public class OrderStagingEmailVmQuery : GetByIdQuery, IRequest<OrderStagingEmailVm>
{
}
public class OrderStagingEmailVmQueryHandler : BaseQueryHandler, IRequestHandler<OrderStagingEmailVmQuery, OrderStagingEmailVm>
{
    private readonly ContactReportSettings _contactReportSettings;
    public OrderStagingEmailVmQueryHandler(IAppDbContext context, IMapper mapper, IOptions<ContactReportSettings> contactReportSettings) : base(context, mapper)
    {
        _contactReportSettings = contactReportSettings.Value;
    }

    public async Task<OrderStagingEmailVm> Handle(OrderStagingEmailVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.OrderStagings.IgnoreQueryFilters()
                                          .SingleAsync(x => x.OrderStagingId == request.Id, cancellationToken);

        var currentUser = await _context.Users.Where(e => e.Email == entity.CreatedBy)
                                                      .FirstOrDefaultAsync(cancellationToken);

        var vm = _mapper.Map<OrderStaging, OrderStagingEmailVm>(entity);

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