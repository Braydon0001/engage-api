using Engage.Application.Services.EmployeeDivisions.Queries;
using Engage.Application.Services.EmployeeJobTitles.Queries;
using Engage.Application.Services.Employees.Queries;
using Engage.Application.Services.EngageDepartments.Models;
using Engage.Application.Services.EngageRegions.Models;
using Engage.Application.Services.StoreFormats.Queries;
using Engage.Application.Services.Stores.Queries;

namespace Engage.Application.Services.WebFileTargets.Queries;

public class WebFileTargetsQuery : IRequest<WebFileTargets>
{
    public int Id { get; set; }
}

public class WebFileTargetsHandler : IRequestHandler<WebFileTargetsQuery, WebFileTargets>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public WebFileTargetsHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<WebFileTargets> Handle(WebFileTargetsQuery query, CancellationToken cancellationToken)
    {
        var entity = await _context.WebFiles.SingleOrDefaultAsync(e => e.WebFileId == query.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var entities = await _context.WebFileTargets.AsQueryable().AsNoTracking().Where(e => e.WebFileId == query.Id).ToListAsync(cancellationToken);

        var employeeDivisionIds = entities.OfType<WebFileEmployeeDivision>().Select(e => e.EmployeeDivisionId).ToList();
        var engageDepartmentIds = entities.OfType<WebFileEngageDepartment>().Select(e => e.EngageDepartmentId).ToList();
        var employeeJobTitleIds = entities.OfType<WebFileEmployeeJobTitle>().Select(e => e.EmployeeJobTitleId).ToList();
        var employeeIds = entities.OfType<WebFileEmployee>().Select(e => e.EmployeeId).ToList();
        var engageRegionIds = entities.OfType<WebFileEngageRegion>().Select(e => e.EngageRegionId).ToList();
        var storeFormatIds = entities.OfType<WebFileStoreFormat>().Select(e => e.StoreFormatId).ToList();
        var storeIds = entities.OfType<WebFileStore>().Select(e => e.StoreId).ToList();

        var employeeDivisions = await _context.EmployeeDivisions.Where(e => employeeDivisionIds.Contains(e.EmployeeDivisionId)).ProjectTo<EmployeeDivisionDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        var engageDepartments = await _context.EngageDepartments.Where(e => engageDepartmentIds.Contains(e.Id)).ProjectTo<EngageDepartmentDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        var employeeJobTitles = await _context.EmployeeJobTitles.Where(e => employeeJobTitleIds.Contains(e.EmployeeJobTitleId)).ProjectTo<EmployeeJobTitleDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        var employees = await _context.Employees.Where(e => employeeIds.Contains(e.EmployeeId)).ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        var enageRegions = await _context.EngageRegions.Where(e => engageRegionIds.Contains(e.Id)).ProjectTo<EngageRegionDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        var storeFormats = await _context.StoreFormats.Where(e => storeFormatIds.Contains(e.Id)).ProjectTo<StoreFormatDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        var stores = await _context.Stores.Where(e => storeIds.Contains(e.StoreId)).ProjectTo<StoreDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);

        return new WebFileTargets(employeeDivisions, engageDepartments, employeeJobTitles, employees, enageRegions, storeFormats, stores);
    }
}
