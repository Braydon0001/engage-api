namespace Engage.Application.Services.EngageDepartments.Queries;

public class EngageDepartmentsListQuery : IRequest<List<OptionDto>>
{
    public bool FilterBySubGroup { get; set; } = false;
}
public record EngageDepartmentsListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<EngageDepartmentsListQuery, List<OptionDto>>
{
    public async Task<List<OptionDto>> Handle(EngageDepartmentsListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.EngageDepartments.AsNoTracking().AsQueryable();

        if (query.FilterBySubGroup)
        {
            var subGroups = await Context.EngageSubGroups.AsNoTracking()
                                                   .Where(e => e.Disabled == false && e.Deleted == false)
                                                   .Select(e => e.EngageDepartmentId)
                                                   .Distinct().ToListAsync();

            queryable = queryable.Where(e => subGroups.Contains(e.Id));
        }

        return await queryable.Where(e => e.Disabled == false).Select(e => new OptionDto { Id = e.Id, Name = e.Name }).ToListAsync(cancellationToken);
    }
}