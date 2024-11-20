using Engage.Application.Services.EmployeeRegions.Queries;
using Engage.Application.Services.Employees.Models;

namespace Engage.Application.Services.Employees.Queries;

public class EmployeePaginatedQuery : PaginatedQuery, IRequest<ListResult<EmployeeListDto>>
{
}

public record EmployeePaginatedHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<EmployeePaginatedQuery, ListResult<EmployeeListDto>>
{
    public async Task<ListResult<EmployeeListDto>> Handle(EmployeePaginatedQuery query, CancellationToken cancellationToken)
    {
        var props = EmployeePaginationProps.Create();

        var queryable = Context.Employees.Where(e => e.EmployeeTypeId == (int)EmployeeTypeId.Employee)
                                         .AsQueryable()
                                         .AsNoTracking();

        var engageRegionIds = await Mediator.Send(new UserRegionsQuery(), cancellationToken);

        if (!engageRegionIds.Contains(7))
        {
            queryable = queryable
                                .Join(Context.EmployeeRegions.Where(c => engageRegionIds.Contains(c.EngageRegionId)),
                                      employee => employee.EmployeeId,
                                      region => region.EmployeeId,
                                      (employee, region) => employee).Distinct();
        }

        #region Custom Filters
        if (query.FilterModel != null)
        {

            query.FilterModel.TryGetValue("employeeJobTitles", out FilterModel employeeJobTitles);
            if (employeeJobTitles != null && employeeJobTitles.Values.Count > 0)
            {
                var employeeIds = await Context.EmployeeEmployeeJobTitles.Where(e => employeeJobTitles.Values.Contains(e.EmployeeJobTitleId))
                                                                          .Select(e => e.EmployeeId)
                                                                          .ToListAsync(cancellationToken);
                queryable = queryable.Where(e => employeeIds.Contains(e.EmployeeId));
            }

            query.FilterModel.TryGetValue("engageRegions", out FilterModel engageRegions);
            if (engageRegions != null && engageRegions.Values.Count > 0)
            {
                var employeeIds = await Context.EmployeeRegions.Where(e => engageRegions.Values.Contains(e.EngageRegionId))
                                                                          .Select(e => e.EmployeeId)
                                                                          .ToListAsync(cancellationToken);
                queryable = queryable.Where(e => employeeIds.Contains(e.EmployeeId));
            }

            query.FilterModel.TryGetValue("engageSubRegion", out FilterModel engageSubRegion);
            if (engageSubRegion != null && engageSubRegion.Values.Count > 0)
            {
                queryable = queryable.Where(e => e.EngageSubRegionId.HasValue && engageSubRegion.Values.Contains(e.EngageSubRegionId.Value));
            }

            query.FilterModel.TryGetValue("engageDepartments", out FilterModel engageDepartments);
            if (engageDepartments != null && engageDepartments.Values.Count > 0)
            {
                var employeeIds = await Context.EmployeeDepartments.Where(e => engageDepartments.Values.Contains(e.EngageDepartmentId))
                                                                          .Select(e => e.EmployeeId)
                                                                          .ToListAsync(cancellationToken);
                queryable = queryable.Where(e => employeeIds.Contains(e.EmployeeId));
            }

            query.FilterModel.TryGetValue("name", out FilterModel name);
            if (name != null && name.Filter.IsNotNullOrWhiteSpace())
            {
                queryable = queryable.Where(e => EF.Functions.Like(e.User.FullName, $"%{name.Filter}%"));
            }
        }
        #endregion

        if (query.SortModel.IsNullOrEmpty())
        {
            queryable = queryable.OrderBy(e => e.FirstName)
                                 .ThenBy(e => e.LastName);
        }

        var entities = await queryable.Filter(query, props)
                                      .Sort(query, props)
                                      .SkipQuery(query)
                                      .TakeQuery(query)
                                      .ProjectTo<EmployeeListDto>(Mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return new(entities);
    }
}
