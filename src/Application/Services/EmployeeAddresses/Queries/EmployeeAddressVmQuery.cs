using Engage.Application.Services.EmployeeAddresses.Models;

namespace Engage.Application.Services.EmployeeAddresses.Queries;

public class EmployeeAddressVmQuery : IRequest<EmployeeAddressVm>
{
    public int Id { get; set; }
}

public class EmployeeAddressVmQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeAddressVmQuery, EmployeeAddressVm>
{
    public EmployeeAddressVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeAddressVm> Handle(EmployeeAddressVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeAddresses
                                        .Include(x => x.Country)
                                        .Include(x => x.Province)
                                        .Include(x => x.PostalCountry)
                                        .Include(x => x.PostalProvince)
                                        .FirstOrDefaultAsync(x => x.EmployeeAddressId == request.Id, cancellationToken);

        return _mapper.Map<EmployeeAddress, EmployeeAddressVm>(entity);
    }
}
