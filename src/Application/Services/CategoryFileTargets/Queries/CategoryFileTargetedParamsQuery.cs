using Engage.Application.Services.CategoryFiles.Queries;

namespace Engage.Application.Services.CategoryFileEmployees.Queries;

public record CategoryFileTargetedParamsQuery(int EmployeeId, int StoreId, DateTime Date, List<int> FileTypeId, string Search) : IRequest<CategoryFileAdvancedTargetingVm>
{
}

public class CategoryFileTargetedParamsHandler : IRequestHandler<CategoryFileTargetedParamsQuery, CategoryFileAdvancedTargetingVm>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public CategoryFileTargetedParamsHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CategoryFileAdvancedTargetingVm> Handle(CategoryFileTargetedParamsQuery query, CancellationToken cancellationToken)
    {
        var employee = await _context.Employees.Include(e => e.EmployeeJobTitles).ThenInclude(e => e.EmployeeJobTitle)
                                               .Include(e => e.EmployeeRegions).ThenInclude(e => e.EngageRegion)
                                               .SingleOrDefaultAsync(e => e.EmployeeId == query.EmployeeId, cancellationToken);

        var store = await _context.Stores.Include(e => e.StoreCategoryGroups).ThenInclude(e => e.CategoryGroup)
                                         .SingleOrDefaultAsync(e => e.StoreId == query.StoreId, cancellationToken);

        if (employee == null || store == null)
        {
            return null;
        }

        var targetedCategoryFiles = await _context.CategoryFileTargets.Select(e => e.CategoryFileId).ToListAsync(cancellationToken);

        var employeeCategoryFileIds = await _context.CategoryFileEmployees.Where(e => e.EmployeeId == query.EmployeeId
                                                                            && query.Date.Date >= e.CategoryFile.StartDate.Date
                                                                            && (!e.CategoryFile.EndDate.HasValue
                                                                            || query.Date.Date <= e.CategoryFile.EndDate.Value.Date))
                                                                          .Select(e => e.CategoryFileId)
                                                                          .ToListAsync(cancellationToken);

        var jobTitleIds = employee.EmployeeJobTitles.Where(e => e.EmployeeJobTitle.Disabled == false
                                                        && e.EmployeeJobTitle.Level == 3)
                                                    .Select(e => e.EmployeeJobTitleId)
                                                    .ToList();

        var jobTitleCategoryFileIds = await _context.CategoryFileEmployeeJobTitles.Where(e => jobTitleIds
                                                                                  .Contains(e.EmployeeJobTitleId)
                                                                                      && e.EmployeeJobTitle.Disabled == false
                                                                                      && e.EmployeeJobTitle.Level == 3)
                                                                                  .Select(e => e.CategoryFileId)
                                                                                  .ToListAsync(cancellationToken);

        var subGroupIds = await _context.EmployeeStores.Where(e => e.EmployeeId == query.EmployeeId && e.StoreId == query.StoreId)
                                                       .Select(e => e.EngageSubGroupId)
                                                       .Distinct()
                                                       .ToListAsync(cancellationToken);

        var subGroupCategoryFileIds = await _context.CategoryFileEngageSubGroups.Where(e => subGroupIds
                                                                                .Contains(e.EngageSubGroupId))
                                                                                .Select(e => e.CategoryFileId)
                                                                                .ToListAsync(cancellationToken);

        var regionIds = employee.EmployeeRegions.Where(e => e.EngageRegion.Disabled == false).Select(e => e.EngageRegionId).ToList();

        var regionCategoryFileIds = await _context.CategoryFileEngageRegions.Where(e => regionIds.Contains(e.EngageRegionId)
                                                                                && e.EngageRegion.Disabled == false)
                                                                            .Select(e => e.CategoryFileId)
                                                                            .ToListAsync(cancellationToken);

        var storeCategoryFileIds = await _context.CategoryFileStores.Where(e => e.StoreId == query.StoreId && query.Date.Date >= e.CategoryFile.StartDate.Date && (!e.CategoryFile.EndDate.HasValue || query.Date.Date <= e.CategoryFile.EndDate.Value.Date))
                                                                          .Select(e => e.CategoryFileId)
                                                                          .ToListAsync(cancellationToken);

        var formatCategoryFileIds = await _context.CategoryFileStoreFormats.Where(e => store.StoreFormatId == e.StoreFormatId)
                                                                           .Select(e => e.CategoryFileId)
                                                                           .ToListAsync(cancellationToken);

        var categoryGroupIds = store.StoreCategoryGroups.Where(e => e.CategoryGroup.Disabled == false).Select(e => e.CategoryGroupId).ToList();
        var categoryFileCategoryGroupIds = await _context.CategoryFileCategoryGroups.Where(e => categoryGroupIds.Contains(e.CategoryGroupId))
                                                                               .Select(e => e.CategoryFileId)
                                                                               .ToListAsync(cancellationToken);

        var categoryFileIds = employeeCategoryFileIds.Union(jobTitleCategoryFileIds)
                                                     .Union(subGroupCategoryFileIds)
                                                     .Union(regionCategoryFileIds)
                                                     .Union(storeCategoryFileIds)
                                                     .Union(formatCategoryFileIds)
                                                     .Union(categoryFileCategoryGroupIds)
                                                     .Distinct()
                                                     .ToList();



        var queryable = _context.CategoryFiles.AsNoTracking()
                                              .AsQueryable()
                                              .Where(e => e.Disabled == false &&
                                                     query.Date.Date >= e.StartDate.Date
                                                     && (!e.EndDate.HasValue || query.Date.Date <= e.EndDate.Value.Date)
                                                     && ((targetedCategoryFiles.Contains(e.CategoryFileId)
                                                     && categoryFileIds.Contains(e.CategoryFileId))
                                                     //Include all files which are not targted
                                                     //|| !targetedCategoryFiles.Contains(e.CategoryFileId)
                                                     ));

        if (query.FileTypeId != null && query.FileTypeId.Any())
        {
            queryable = queryable.Where(e => query.FileTypeId.Contains(e.CategoryFileTypeId));
        }

        if (!string.IsNullOrEmpty(query.Search))
        {
            queryable = queryable.Where(e => EF.Functions.Like(e.Name, $"%{query.Search}%"));
        }

        var categoryFiles = await queryable.ProjectTo<CategoryFileDto>(_mapper.ConfigurationProvider)
                                           .ToListAsync(cancellationToken);

        var hasRules = categoryFiles.Any(e => e.TargetRule != null);

        return new CategoryFileAdvancedTargetingVm()
        {
            HasAdvancedTargeting = hasRules,
            EmployeeId = new List<int> { employee.EmployeeId },
            EmployeeJobTitleIds = jobTitleIds,
            EmployeeSubGroupIds = subGroupIds,
            EmployeeEngageRegionIds = regionIds,
            StoreId = new List<int> { store.StoreId },
            StoreFormatIds = new List<int> { store.StoreFormatId },
            StoreCategoryGroupIds = categoryGroupIds,
            CategoryFiles = new ListResult<CategoryFileDto>(categoryFiles)
        };
    }
}