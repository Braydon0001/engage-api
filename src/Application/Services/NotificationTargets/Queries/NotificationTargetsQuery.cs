using Engage.Application.Services.EmployeeJobTitles.Queries;
using Engage.Application.Services.Employees.Queries;
using Engage.Application.Services.EngageRegions.Models;
using Engage.Application.Services.StoreFormats.Queries;
using Engage.Application.Services.Stores.Queries;

namespace Engage.Application.Services.NotificationTargets.Queries;

public class NotificationTargetsQuery : IRequest<NotificationTargets>
{
    public int Id { get; set; }
}

public class NotificationTargetsHandler : IRequestHandler<NotificationTargetsQuery, NotificationTargets>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public NotificationTargetsHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<NotificationTargets> Handle(NotificationTargetsQuery query, CancellationToken cancellationToken)
    {
        var entity = await _context.Notifications.SingleOrDefaultAsync(e => e.NotificationId == query.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var entities = await _context.NotificationTargets.AsQueryable().AsNoTracking().Where(e => e.NotificationId == query.Id).ToListAsync(cancellationToken);

        var employeeJobTitleIds = entities.OfType<NotificationEmployeeJobTitle>().Select(e => e.EmployeeJobTitleId).ToList();
        var employeeIds = entities.OfType<NotificationEmployee>().Select(e => e.EmployeeId).ToList();
        var engageRegionIds = entities.OfType<NotificationEngageRegion>().Select(e => e.EngageRegionId).ToList();
        var storeFormatIds = entities.OfType<NotificationStoreFormat>().Select(e => e.StoreFormatId).ToList();
        var storeIds = entities.OfType<NotificationStore>().Select(e => e.StoreId).ToList();

        var employeeJobTitles = await _context.EmployeeJobTitles.Where(e => employeeJobTitleIds.Contains(e.EmployeeJobTitleId)).ProjectTo<EmployeeJobTitleDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        var employees = await _context.Employees.Where(e => employeeIds.Contains(e.EmployeeId)).ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        var enageRegions = await _context.EngageRegions.Where(e => engageRegionIds.Contains(e.Id)).ProjectTo<EngageRegionDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        var storeFormats = await _context.StoreFormats.Where(e => storeFormatIds.Contains(e.Id)).ProjectTo<StoreFormatDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        var stores = await _context.Stores.Where(e => storeIds.Contains(e.StoreId)).ProjectTo<StoreDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);

        return new NotificationTargets(employeeJobTitles, employees, enageRegions, storeFormats, stores);
    }
}
