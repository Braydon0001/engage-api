namespace Engage.Application.Services.EmployeeJobTitleTypes.Queries;

public record EmployeeJobTitleTypeVmQuery(int Id) : IRequest<EmployeeJobTitleTypeVm>;

public record EmployeeJobTitleTypeVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<EmployeeJobTitleTypeVmQuery, EmployeeJobTitleTypeVm>
{
    public async Task<EmployeeJobTitleTypeVm> Handle(EmployeeJobTitleTypeVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.EmployeeJobTitleTypes.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.EmployeeJobTitle);

        var entity = await queryable.SingleOrDefaultAsync(e => e.EmployeeJobTitleTypeId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<EmployeeJobTitleTypeVm>(entity);
    }
}