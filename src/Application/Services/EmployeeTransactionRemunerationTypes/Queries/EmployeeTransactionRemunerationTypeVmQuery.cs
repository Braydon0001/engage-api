namespace Engage.Application.Services.EmployeeTransactionRemunerationTypes.Queries;

public record EmployeeTransactionRemunerationTypeVmQuery(int Id) : IRequest<EmployeeTransactionRemunerationTypeVm>;

public record EmployeeTransactionRemunerationTypeVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<EmployeeTransactionRemunerationTypeVmQuery, EmployeeTransactionRemunerationTypeVm>
{
    public async Task<EmployeeTransactionRemunerationTypeVm> Handle(EmployeeTransactionRemunerationTypeVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.EmployeeTransactionRemunerationTypes.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.EmployeeTransactionRemunerationTypeId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<EmployeeTransactionRemunerationTypeVm>(entity);
    }
}