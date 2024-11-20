using Engage.Application.Services.EmployeeJobTitles.Models;

namespace Engage.Application.Services.EmployeeJobTitles.Queries;

public class EmployeeJobTitlesHierarchyQuery : GetQuery, IRequest<ListResult<EmployeeJobTitleHierarchyDto>>
{
    public int Level { get; set; }
}

public class EmployeeJobTitlesHierarchyHandler : BaseQueryHandler, IRequestHandler<EmployeeJobTitlesHierarchyQuery, ListResult<EmployeeJobTitleHierarchyDto>>
{
    public EmployeeJobTitlesHierarchyHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<EmployeeJobTitleHierarchyDto>> Handle(EmployeeJobTitlesHierarchyQuery query, CancellationToken cancellationToken)
    {
        var dtos = new List<EmployeeJobTitleHierarchyDto>();

        var queryable = _context.EmployeeJobTitles.AsQueryable().AsNoTracking();

        var groups = queryable.Where(e => e.Level == 1)
                              .OrderBy(e => e.Name);

        var subGroups = queryable.Include(e => e.Parent)
                                 .Where(e => e.Level == 2)
                                 .OrderBy(e => e.Name);

        var titles = queryable.Include(e => e.Parent)
                              .ThenInclude(e => e.Parent)
                              .Where(e => e.Level == 3)
                              .OrderBy(e => e.Name);

        if (query.Level == 1)
        {
            var groupDtos = await groups.Select(e => new EmployeeJobTitleHierarchyDto(e.EmployeeJobTitleId, 1, new string[] { e.Name }))
                                        .ToListAsync(cancellationToken);

            var subGroupDtos = await subGroups.Select(e => new EmployeeJobTitleHierarchyDto(e.EmployeeJobTitleId, 2, new string[] { e.Parent.Name, e.Name }))
                                              .ToListAsync(cancellationToken);

            var titleDtos = await queryable.Select(e => new EmployeeJobTitleHierarchyDto(e.EmployeeJobTitleId, 3, new string[] { e.Parent.Parent.Name, e.Parent.Name, e.Name }))
                                           .ToListAsync(cancellationToken);

            dtos.AddRange(groupDtos);
            dtos.AddRange(subGroupDtos);
            dtos.AddRange(titleDtos);
        }

        if (query.Level == 2)
        {
            var subGroupDtos = await subGroups.Select(e => new EmployeeJobTitleHierarchyDto(e.EmployeeJobTitleId, 2, new string[] { e.Name }))
                                              .ToListAsync(cancellationToken);

            var titleDtos = await queryable.Select(e => new EmployeeJobTitleHierarchyDto(e.EmployeeJobTitleId, 3, new string[] { e.Parent.Name, e.Name }))
                                           .ToListAsync(cancellationToken);

            dtos.AddRange(subGroupDtos);
            dtos.AddRange(titleDtos);

        }

        if (query.Level == 3)
        {
            var titleDtos = await queryable.Select(e => new EmployeeJobTitleHierarchyDto(e.EmployeeJobTitleId, 3, new string[] { e.Name }))
                                           .ToListAsync(cancellationToken);
            dtos.AddRange(titleDtos);
        }

        return new ListResult<EmployeeJobTitleHierarchyDto>(dtos);
    }
}
