using Engage.Application.Services.Suppliers.Models;

namespace Engage.Application.Services.Suppliers.Queries;

public class UserSupplierVmQuery : IRequest<SupplierVm>
{
}

public class UserSupplierVmQueryHandler : BaseQueryHandler, IRequestHandler<UserSupplierVmQuery, SupplierVm>
{
    private readonly IUserService _user;

    public UserSupplierVmQueryHandler(IAppDbContext context, IMapper mapper, IUserService user) : base(context, mapper)
    {
        _user = user;
    }

    public async Task<SupplierVm> Handle(UserSupplierVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Suppliers.Include(x => x.SupplierGroup)
                                             .Include(x => x.SupplierSupplierTypes)
                                             .ThenInclude(x => x.SupplierType)
                                             .Include(x => x.SupplierEngageBrands)
                                             .ThenInclude(x => x.EngageBrand)
                                             .FirstOrDefaultAsync(x => x.SupplierId == _user.SupplierId, cancellationToken);

        return _mapper.Map<Supplier, SupplierVm>(entity);
    }
}
