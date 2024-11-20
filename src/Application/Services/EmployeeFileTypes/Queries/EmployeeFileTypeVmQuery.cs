namespace Engage.Application.Services.EmployeeFileTypes.Queries;

public class EmployeeFileTypeVmQuery : IRequest<EmployeeFileTypeVm>
{
    public int Id { get; set; }
}

public class EmployeeFileTypeVmHandler : VmQueryHandler, IRequestHandler<EmployeeFileTypeVmQuery, EmployeeFileTypeVm>
{
    public EmployeeFileTypeVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeFileTypeVm> Handle(EmployeeFileTypeVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeeFileTypes.AsQueryable().AsNoTracking();

        var entity = await queryable.SingleOrDefaultAsync(e => e.EmployeeFileTypeId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<EmployeeFileTypeVm>(entity);
    }
}