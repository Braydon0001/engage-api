using Engage.Application.Services.EngageGroups.Models;

namespace Engage.Application.Services.EngageGroups.Queries;

public class EngageGroupHierarchyQuery : IRequest<ListResult<EngageGroupHierarchyDto>>
{
    public int Level { get; set; }
}


public class EngageGroupHierarchyHandler : BaseQueryHandler, IRequestHandler<EngageGroupHierarchyQuery, ListResult<EngageGroupHierarchyDto>>
{
    public EngageGroupHierarchyHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<EngageGroupHierarchyDto>> Handle(EngageGroupHierarchyQuery query, CancellationToken cancellationToken)
    {
        var dtos = new List<EngageGroupHierarchyDto>();

        var groups = _context.EngageGroups.AsQueryable()
                                          .AsNoTracking()
                                          .OrderBy(e => e.Name);

        var subGroups = _context.EngageSubGroups.AsQueryable()
                                                .AsNoTracking()
                                                .Include(e => e.EngageGroup)
                                                .OrderBy(e => e.Name);

        var categories = _context.EngageCategories.AsQueryable()
                                                  .AsNoTracking()
                                                  .Include(e => e.EngageSubGroup)
                                                  .ThenInclude(e => e.EngageGroup)
                                                  .OrderBy(e => e.Name);

        var subCategories = _context.EngageSubCategories.AsQueryable()
                                                        .AsNoTracking()
                                                        .Include(e => e.EngageCategory)
                                                        .ThenInclude(e => e.EngageSubGroup)
                                                        .ThenInclude(e => e.EngageGroup)
                                                        .OrderBy(e => e.Name);

        if (query.Level == 1)
        {
            var groupDtos = await groups.Select(e => new EngageGroupHierarchyDto(e.Id, 1, new string[] { e.Name }))
                                         .ToListAsync(cancellationToken);

            var subGroupDtos = await subGroups.Select(e => new EngageGroupHierarchyDto(e.Id, 2, new string[] { e.EngageGroup.Name, e.Name }))
                                         .ToListAsync(cancellationToken);

            var categoryDtos = await categories.Select(e => new EngageGroupHierarchyDto(e.Id, 3, new string[] { e.EngageSubGroup.EngageGroup.Name, e.EngageSubGroup.Name, e.Name }))
                                         .ToListAsync(cancellationToken);

            var subCategoryDtos = await subCategories.Select(e => new EngageGroupHierarchyDto(e.Id, 4, new string[] { e.EngageCategory.EngageSubGroup.EngageGroup.Name, e.EngageCategory.EngageSubGroup.Name, e.EngageCategory.Name, e.Name }))
                                         .ToListAsync(cancellationToken);

            dtos.AddRange(groupDtos);
            dtos.AddRange(subGroupDtos);
            dtos.AddRange(categoryDtos);
            dtos.AddRange(subCategoryDtos);
        }

        if (query.Level == 2)
        {
            var subGroupDtos = await subGroups.Select(e => new EngageGroupHierarchyDto(e.Id, 2, new string[] { e.Name }))
                                              .ToListAsync(cancellationToken);

            var categoryDtos = await categories.Select(e => new EngageGroupHierarchyDto(e.Id, 3, new string[] { e.EngageSubGroup.Name, e.Name }))
                                               .ToListAsync(cancellationToken);

            var subCategoryDtos = await subCategories.Select(e => new EngageGroupHierarchyDto(e.Id, 4, new string[] { e.EngageCategory.EngageSubGroup.Name, e.EngageCategory.Name, e.Name }))
                                                     .ToListAsync(cancellationToken);

            dtos.AddRange(subGroupDtos);
            dtos.AddRange(categoryDtos);
            dtos.AddRange(subCategoryDtos);
        }

        if (query.Level == 3)
        {
            var categoryDtos = await categories.Select(e => new EngageGroupHierarchyDto(e.Id, 3, new string[] { e.Name }))
                                               .ToListAsync(cancellationToken);

            var subCategoryDtos = await subCategories.Select(e => new EngageGroupHierarchyDto(e.Id, 4, new string[] { e.EngageCategory.Name, e.Name }))
                                                     .ToListAsync(cancellationToken);

            dtos.AddRange(categoryDtos);
            dtos.AddRange(subCategoryDtos);
        }

        if (query.Level == 4)
        {

            var subCategoryDtos = await subCategories.Select(e => new EngageGroupHierarchyDto(e.Id, 4, new string[] { e.Name }))
                                                     .ToListAsync(cancellationToken);

            dtos.AddRange(subCategoryDtos);
        }

        return new ListResult<EngageGroupHierarchyDto>(dtos);
    }
}
