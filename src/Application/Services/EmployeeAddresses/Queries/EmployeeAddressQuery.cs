using Engage.Application.Services.EmployeeAddresses.Models;

namespace Engage.Application.Services.EmployeeAddresses.Queries
{
    public class EmployeeAddressQuery : GetByIdQuery, IRequest<EmployeeAddressDto>
    {
    }

    public class EmployeeAddressQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeAddressQuery, EmployeeAddressDto>
    {
        public EmployeeAddressQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<EmployeeAddressDto> Handle(EmployeeAddressQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.EmployeeAddresses.Include(x => x.Employee)
                                                .Include(x => x.Country)
                                                .Include(x => x.Province)
                                                .Include(x => x.PostalCountry)
                                                .Include(x => x.PostalProvince)
                                                .FirstOrDefaultAsync(x => x.EmployeeAddressId == request.Id, cancellationToken);

            return _mapper.Map<EmployeeAddress, EmployeeAddressDto>(entity);
        }
    }
}
