namespace Engage.Application.Services.EmployeeFiles.Queries;

public class EmployeeFileVmQuery : IRequest<EmployeeFileVm>
{
    public int Id { get; set; }
}

public class EmployeeFileVmHandler : VmQueryHandler, IRequestHandler<EmployeeFileVmQuery, EmployeeFileVm>
{
    public EmployeeFileVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeFileVm> Handle(EmployeeFileVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeeFiles.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.Employee)
                             .Include(e => e.EmployeeFileType);

        var entity = await queryable.SingleOrDefaultAsync(e => e.EmployeeFileId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<EmployeeFileVm>(entity);
    }
}