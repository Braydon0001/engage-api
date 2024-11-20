using Engage.Application.Services.EmployeeStoreCheckIns.Models;

namespace Engage.Application.Services.EmployeeStoreCheckIns.Queries;

public class GetEmployeeStoreCheckInQuery : GetByIdQuery, IRequest<EmployeeStoreCheckInDto>
{ }

public class GetEmployeeStoreCheckInQueryHandler : BaseQueryHandler, IRequestHandler<GetEmployeeStoreCheckInQuery, EmployeeStoreCheckInDto>
{
    public GetEmployeeStoreCheckInQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<EmployeeStoreCheckInDto> Handle(GetEmployeeStoreCheckInQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeStoreCheckIns.FirstOrDefaultAsync(x => x.EmployeeStoreCheckInId == request.Id, cancellationToken);

        return _mapper.Map<EmployeeStoreCheckIn, EmployeeStoreCheckInDto>(entity);
    }
}
