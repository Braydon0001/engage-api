using Engage.Application.Services.WebFileCategories.Queries;
using Engage.Application.Services.WebFiles.Queries;

namespace Engage.Application.Services.WebFileEmployees.Queries;

public record WebFileEmployeeTargeted(IEnumerable<WebFileCategoryDto> Categories, IEnumerable<WebFileDto> Files, Dictionary<string, List<JsonFile>> EmployeeFiles)
{
}

public record WebFileEmployeeTargetedQuery(int EmployeeId, DateTime Date) : IRequest<WebFileEmployeeTargeted>
{
}

public class WebFileEmployeeTargetedHandler : IRequestHandler<WebFileEmployeeTargetedQuery, WebFileEmployeeTargeted>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public WebFileEmployeeTargetedHandler(IAppDbContext context, IMapper mapper, IMediator mediator)
    {
        _context = context;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<WebFileEmployeeTargeted> Handle(WebFileEmployeeTargetedQuery query, CancellationToken cancellationToken)
    {
        var employee = await _context.Employees.Include(e => e.EmployeeJobTitles).ThenInclude(e => e.EmployeeJobTitle)
                                               .Include(e => e.EmployeeRegions).ThenInclude(e => e.EngageRegion)
                                               .Include(e => e.EmployeeDivisions).ThenInclude(e => e.EmployeeDivision)
                                               .Include(e => e.EmployeeDepartments).ThenInclude(e => e.EngageDepartment)
                                               .SingleOrDefaultAsync(e => e.EmployeeId == query.EmployeeId, cancellationToken);
        if (employee == null)
        {
            return null;
        }

        var categories = await _context.WebFileCategories.Where(e => e.WebFileGroupId == (int)WebFileGroupEnum.Employee)
                                                         .ProjectTo<WebFileCategoryDto>(_mapper.ConfigurationProvider)
                                                         .ToListAsync(cancellationToken);
        var categoryIds = categories.Select(e => e.Id).ToList();

        var employeeWebFileIds = await _context.WebFileEmployees.Where(e => e.EmployeeId == query.EmployeeId && query.Date >= e.WebFile.StartDate && (!e.WebFile.EndDate.HasValue || query.Date <= e.WebFile.EndDate))
                                                                .Select(e => e.WebFileId)
                                                                .ToListAsync(cancellationToken);

        var divisions = employee.EmployeeDivisions.Where(e => e.EmployeeDivision.Disabled == false).Select(e => e.EmployeeDivisionId).ToList();
        var fileDivisons = await _context.WebFileEmployeeDivisions.Where(e => divisions.Contains(e.EmployeeDivisionId)).Select(e => e.WebFileId).ToListAsync(cancellationToken);

        var departments = employee.EmployeeDepartments.Where(e => e.EngageDepartment.Disabled == false).Select(e => e.EngageDepartmentId).ToList();
        var departmentFileIds = await _context.WebFileEngageDepartments.Where(e => departments.Contains(e.EngageDepartmentId)).Select(e => e.WebFileId).ToListAsync(cancellationToken);

        var jobTitleIds = employee.EmployeeJobTitles.Where(e => e.EmployeeJobTitle.Disabled == false && e.EmployeeJobTitle.Level == 3).Select(e => e.EmployeeJobTitleId).ToList();
        var JobTitleWebFileIds = await _context.WebFileEmployeeJobTitles.Where(e => jobTitleIds.Contains(e.EmployeeJobTitleId) && e.EmployeeJobTitle.Disabled == false && e.EmployeeJobTitle.Level == 3)
                                                                        .Select(e => e.WebFileId)
                                                                        .ToListAsync(cancellationToken);

        var regionIds = employee.EmployeeRegions.Where(e => e.EngageRegion.Disabled == false).Select(e => e.EngageRegionId).ToList();
        var regionWebFileIds = await _context.WebFileEngageRegions.Where(e => regionIds.Contains(e.EngageRegionId) && e.EngageRegion.Disabled == false)
                                                                  .Select(e => e.WebFileId)
                                                                  .ToListAsync(cancellationToken);

        var webFileIds = employeeWebFileIds.Union(JobTitleWebFileIds).Union(regionWebFileIds).Union(fileDivisons).Union(departmentFileIds).Distinct().ToList();


        var webFiles = await _context.WebFiles.Where(e => e.Disabled == false &&
                                                          query.Date >= e.StartDate && (!e.EndDate.HasValue || query.Date <= e.EndDate) &&
                                                          categoryIds.Contains(e.WebFileCategoryId) &&
                                                          ((e.TargetStrategyId != (int)TargetStrategyEnum.All && webFileIds.Contains(e.WebFileId)) ||
                                                            e.TargetStrategyId == (int)TargetStrategyEnum.All ||
                                                           (e.NPrintingId != null && e.EmployeeId == query.EmployeeId)))
                                              .ProjectTo<WebFileDto>(_mapper.ConfigurationProvider)
                                              .ToListAsync(cancellationToken);

        var files = webFiles.Select(e =>
                               {
                                   if (e.Files != null && e.Files.Count == 1)
                                   {
                                       var jsonFile = e.Files[0];
                                       e.FileUrl = jsonFile.Url;
                                       e.FileName = jsonFile.Name;
                                   }
                                   return e;
                               })
                               .OrderByDescending(e => e.Created)
                               .ToList();


        var employeeFiles = await _mediator.Send(new WebFileEmployeeFilesQuery(query.EmployeeId), cancellationToken);

        return new WebFileEmployeeTargeted(categories, files, employeeFiles);
    }
}
