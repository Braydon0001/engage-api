namespace Engage.Application.Services.ExpenseTypes.Queries;

public record ExpenseTypeVmQuery(int Id) : IRequest<ExpenseTypeVm>;

public record ExpenseTypeVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ExpenseTypeVmQuery, ExpenseTypeVm>
{
    public async Task<ExpenseTypeVm> Handle(ExpenseTypeVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ExpenseTypes.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.ExpenseTypeId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<ExpenseTypeVm>(entity);
    }
}