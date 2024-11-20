namespace Engage.Application.Services.EmployeeJobTitleTimes.Queries;

public record EmployeeJobTitleTimeVmQuery(int Id) : IRequest<EmployeeJobTitleTimeVm>;

public record EmployeeJobTitleTimeVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<EmployeeJobTitleTimeVmQuery, EmployeeJobTitleTimeVm>
{
    public async Task<EmployeeJobTitleTimeVm> Handle(EmployeeJobTitleTimeVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.EmployeeJobTitleTimes.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.EmployeeJobTitle);

        var entity = await queryable.SingleOrDefaultAsync(e => e.EmployeeJobTitleTimeId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<EmployeeJobTitleTimeVm>(entity);
    }
}