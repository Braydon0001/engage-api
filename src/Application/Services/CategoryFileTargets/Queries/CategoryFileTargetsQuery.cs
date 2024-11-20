using Engage.Application.Services.CategoryGroups.Queries;
using Engage.Application.Services.EmployeeJobTitles.Queries;
using Engage.Application.Services.Employees.Queries;
using Engage.Application.Services.EngageRegions.Models;
using Engage.Application.Services.EngageSubGroups.Models;
using Engage.Application.Services.StoreFormats.Queries;
using Engage.Application.Services.Stores.Queries;

namespace Engage.Application.Services.CategoryFileTargets.Queries;

public class CategoryFileTargetsQuery : IRequest<CategoryFileTargets>
{
    public int Id { get; set; }
}

public class CategoryFileTargetsHandler : IRequestHandler<CategoryFileTargetsQuery, CategoryFileTargets>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public CategoryFileTargetsHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CategoryFileTargets> Handle(CategoryFileTargetsQuery query, CancellationToken cancellationToken)
    {
        var entity = await _context.CategoryFiles.SingleOrDefaultAsync(e => e.CategoryFileId == query.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var entities = await _context.CategoryFileTargets.AsQueryable().AsNoTracking().Where(e => e.CategoryFileId == query.Id).ToListAsync(cancellationToken);

        var employeeJobTitleIds = entities.OfType<CategoryFileEmployeeJobTitle>().Select(e => e.EmployeeJobTitleId).ToList();
        var employeeIds = entities.OfType<CategoryFileEmployee>().Select(e => e.EmployeeId).ToList();
        var engageRegionIds = entities.OfType<CategoryFileEngageRegion>().Select(e => e.EngageRegionId).ToList();
        var storeFormatIds = entities.OfType<CategoryFileStoreFormat>().Select(e => e.StoreFormatId).ToList();
        var storeIds = entities.OfType<CategoryFileStore>().Select(e => e.StoreId).ToList();
        var categoryGroupIds = entities.OfType<CategoryFileCategoryGroup>().Select(e => e.CategoryGroupId).ToList();
        var engageSubGroupIds = entities.OfType<CategoryFileEngageSubGroup>().Select(e => e.EngageSubGroupId).ToList();

        var employeeJobTitles = await _context.EmployeeJobTitles.Where(e => employeeJobTitleIds.Contains(e.EmployeeJobTitleId)).ProjectTo<EmployeeJobTitleDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        var employees = await _context.Employees.Where(e => employeeIds.Contains(e.EmployeeId)).ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        var enageRegions = await _context.EngageRegions.Where(e => engageRegionIds.Contains(e.Id)).ProjectTo<EngageRegionDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        var storeFormats = await _context.StoreFormats.Where(e => storeFormatIds.Contains(e.Id)).ProjectTo<StoreFormatDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        var stores = await _context.Stores.Where(e => storeIds.Contains(e.StoreId)).ProjectTo<StoreDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        var categoryGroups = await _context.CategoryGroups.Where(e => categoryGroupIds.Contains(e.CategoryGroupId)).ProjectTo<CategoryGroupDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        var engageSubGroups = await _context.EngageSubGroups.Where(e => engageSubGroupIds.Contains(e.Id)).ProjectTo<EngageSubGroupDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);

        return new CategoryFileTargets(employeeJobTitles, employees, enageRegions, storeFormats, stores, categoryGroups, engageSubGroups);
    }
}
