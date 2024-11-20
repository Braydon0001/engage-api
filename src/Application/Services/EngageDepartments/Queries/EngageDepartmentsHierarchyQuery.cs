using Engage.Application.Services.EngageDepartments.Models;

namespace Engage.Application.Services.EngageDepartments.Queries;

public class EngageDepartmentsHierarchyQuery : IRequest<ListResult<EngageDepartmentHierarchyDto>>
{
    public int Level { get; set; }
}

public class EngageDepartmentsHierarchyHandler
    : BaseQueryHandler, IRequestHandler<EngageDepartmentsHierarchyQuery, ListResult<EngageDepartmentHierarchyDto>>
{
    public EngageDepartmentsHierarchyHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<EngageDepartmentHierarchyDto>> Handle(EngageDepartmentsHierarchyQuery query, CancellationToken cancellationToken)
    {
        var dtos = new List<EngageDepartmentHierarchyDto>();

        var groups = _context.EngageDepartmentGroups.AsQueryable()
                                                    .AsNoTracking()
                                                    .OrderBy(e => e.Name);


        var departments = _context.EngageDepartments.AsQueryable()
                                                    .AsNoTracking()
                                                    .Include(e => e.EngageDepartmentGroup)
                                                    .OrderBy(e => e.Name);

        var categories = _context.EngageDepartmentCategories.AsQueryable()
                                                            .AsNoTracking()
                                                            .Include(e => e.EngageDepartment)
                                                            .ThenInclude(e => e.EngageDepartmentGroup)
                                                            .OrderBy(e => e.Name);

        if (query.Level == 1)
        {
            await AddLevelOneDtos(dtos, groups, departments, categories, cancellationToken);
        }

        if (query.Level == 2)
        {
            var departmentDtos = await departments.Select(e => new EngageDepartmentHierarchyDto(e.Id, 2, new string[] { e.Name }))
                                                  .ToListAsync(cancellationToken);

            var categoryDtos = await categories.Select(e => new EngageDepartmentHierarchyDto(e.Id, 3, new string[] { e.EngageDepartment.Name, e.Name }))
                                                .ToListAsync(cancellationToken);

            dtos.AddRange(departmentDtos);
            dtos.AddRange(categoryDtos);
        }

        if (query.Level == 3)
        {
            var categoryDtos = await categories.Select(e => new EngageDepartmentHierarchyDto(e.Id, 3, new string[] { e.Name }))
                                               .ToListAsync(cancellationToken);

            dtos.AddRange(categoryDtos);
        }

        if (query.Level == 4)
        {
            await AddLevelOneDtos(dtos, groups, departments, categories, cancellationToken);

            var engageGroupDtos = await _context.EngageGroups.AsQueryable()
                                                             .AsNoTracking()
                                                             .OrderBy(e => e.Name)
                                                             .Select(e => new EngageDepartmentHierarchyDto(e.Id, 4, new string[] { e.Name }))
                                                             .ToListAsync(cancellationToken);

            var engageSubGroupDtos = await _context.EngageSubGroups.AsQueryable()
                                                                   .AsNoTracking()
                                                                   .Include(e => e.EngageGroup)
                                                                   .OrderBy(e => e.Name)
                                                                   .Select(e => new EngageDepartmentHierarchyDto(e.Id, 5, new string[] { e.EngageGroup.Name, e.Name }))
                                                                   .ToListAsync(cancellationToken);

            var engageCategoryDtos = await _context.EngageCategories.AsQueryable()
                                                                    .AsNoTracking()
                                                                    .Include(e => e.EngageSubGroup)
                                                                    .ThenInclude(e => e.EngageGroup)
                                                                    .OrderBy(e => e.Name)
                                                                    .Select(e => new EngageDepartmentHierarchyDto(e.Id, 6, new string[] { e.EngageSubGroup.EngageGroup.Name, e.EngageSubGroup.Name, e.Name }))
                                                                    .ToListAsync(cancellationToken);

            var engageSubCategoryDtos = await _context.EngageSubCategories.AsQueryable()
                                                                          .AsNoTracking()
                                                                          .Include(e => e.EngageCategory)
                                                                          .ThenInclude(e => e.EngageSubGroup)
                                                                          .ThenInclude(e => e.EngageGroup)
                                                                          .OrderBy(e => e.Name)
                                                                          .Select(e => new EngageDepartmentHierarchyDto(e.Id, 7, new string[] { e.EngageCategory.EngageSubGroup.EngageGroup.Name, e.EngageCategory.EngageSubGroup.Name, e.EngageCategory.Name, e.Name }))
                                                                          .ToListAsync(cancellationToken);

            dtos.AddRange(engageGroupDtos);
            dtos.AddRange(engageSubGroupDtos);
            dtos.AddRange(engageCategoryDtos);
            dtos.AddRange(engageSubCategoryDtos);
        }

        return new ListResult<EngageDepartmentHierarchyDto>(dtos);

        static async Task AddLevelOneDtos(List<EngageDepartmentHierarchyDto> dtos, IOrderedQueryable<EngageDepartmentGroup> groups, IOrderedQueryable<EngageDepartment> departments, IOrderedQueryable<EngageDepartmentCategory> categories, CancellationToken cancellationToken)
        {
            var groupDtos = await groups.Select(e => new EngageDepartmentHierarchyDto(e.Id, 1, new string[] { e.Name }))
                                                    .ToListAsync(cancellationToken);

            var departmentDtos = await departments.Select(e => new EngageDepartmentHierarchyDto(e.Id, 2, new string[] { e.EngageDepartmentGroup.Name, e.Name }))
                                                  .ToListAsync(cancellationToken);

            var categoryDtos = await categories.Select(e => new EngageDepartmentHierarchyDto(e.Id, 3, new string[] { e.EngageDepartment.EngageDepartmentGroup.Name, e.EngageDepartment.Name, e.Name }))
                                                .ToListAsync(cancellationToken);

            dtos.AddRange(groupDtos);
            dtos.AddRange(departmentDtos);
            dtos.AddRange(categoryDtos);
        }
    }
}
